using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.LineItems.LineItemAttributes.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes.Query;
using ShagOxServer.Application.Interfaces.Services.LineItems.BasketAttributes.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Caches.Invalidations.LineItems;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Validator;
using ShagOxServer.Application.Services.LineItems.LineItemAttributes.Mapping;
using ShagOxServer.Application.Services.LineItems.LineItemAttributes.Validator;
using ShagOxServer.Domain.Entities.LineItems;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.LineItems.LineItemAttributes.Create;
public class LineItemAttributeCreateService
    : ILineItemAttributeCreateService
{
    private readonly IRepository<LineItemAttribute> _attributeRepository;

    private readonly LineItemAttributeValidator _basketAttributeValidator;
    private readonly AttributeDefinitionValidator _attributeValidator;

    private readonly LineItemAttributeInvalidationService _attributeInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<LineItemAttributeCreateService> _logger;


    public LineItemAttributeCreateService(
        IRepository<LineItemAttribute> attributeRepository,
        ILineItemAttributeQueryRepository _attributeQueryRepository,
        LineItemAttributeValidator basketAttributeValidator,
        AttributeDefinitionValidator attributeValidator,
         LineItemAttributeInvalidationService attributeInvalid,
        IUnitOfWork unitOfWork,
        ILogger<LineItemAttributeCreateService> logger)
    {
        _attributeRepository = attributeRepository;
        _basketAttributeValidator = basketAttributeValidator;
        _attributeValidator = attributeValidator;
        _attributeInvalid = attributeInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        LineItemAttributeCreateRequest request,
        CancellationToken cancellationToken = default)
    {
        var attributeDefinition = await _attributeValidator
            .GetByIdAsync(request.AttributeDefinitionId, cancellationToken);

        if (!attributeDefinition.IsSuccess)
        {
            return Result<CreateResponse>
                .Fail(attributeDefinition.Error);
        }

        var attributeExists = await _basketAttributeValidator
            .NotExistsAsync(attributeDefinition.Value!,
                request.Order, cancellationToken);

        if (!attributeExists.IsSuccess)
        {
            return Result<CreateResponse>
                .Fail(attributeExists.Error);
        }


        var attribute = LineItemAttributeCreater
            .Create(request);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _attributeRepository.Add(attribute);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create line item attribute. " +
                "AttributeDefinitionId: {AttributeDefinitionId}",
                request.AttributeDefinitionId);

            return Result<CreateResponse>.Fail(
                EntityErrorResources.LineItemAttributeCreateFailed);
        }

        await _attributeInvalid.InvalidateCreateAsync(
            LineItemAttributeCacheMapper.ToInfo(
                attribute,
                attributeDefinition.Value!.CategoryId)
            );

        return Result<CreateResponse>.Success(
            new CreateResponse(
                attribute.Id,
                DateTime.UtcNow
        ));
    }
}
