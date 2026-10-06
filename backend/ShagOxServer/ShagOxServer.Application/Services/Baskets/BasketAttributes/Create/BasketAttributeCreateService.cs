using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.BasketAttributes.Query;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketAttributes.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Baskets.BasketAttributes.Mapping;
using ShagOxServer.Application.Services.Baskets.BasketAttributes.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Baskets;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Validator;
using ShagOxServer.Domain.Entities.Baskets;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Baskets.BasketAttributes.Create;
public class BasketAttributeCreateService
    : IBasketAttributeCreateService
{
    private readonly IRepository<BasketAttribute> _attributeRepository;

    private readonly BasketAttributeValidator _basketAttributeValidator;
    private readonly AttributeDefinitionValidator _attributeValidator;

    private readonly BasketAttributeInvalidationService _attributeInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<BasketAttributeCreateService> _logger;


    public BasketAttributeCreateService(
        IRepository<BasketAttribute> attributeRepository,
        IBasketAttributeQueryRepository _attributeQueryRepository,
        BasketAttributeValidator basketAttributeValidator,
        AttributeDefinitionValidator attributeValidator,
         BasketAttributeInvalidationService attributeInvalid,
        IUnitOfWork unitOfWork,
        ILogger<BasketAttributeCreateService> logger)
    {
        _attributeRepository = attributeRepository;
        _basketAttributeValidator = basketAttributeValidator;
        _attributeValidator = attributeValidator;
        _attributeInvalid = attributeInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<CreateResponse>> CreateAsync(
        BasketAttributeCreateRequest request)
    {
        var attributeDefinition = await _attributeValidator
            .GetByIdAsync(request.AttributeDefinitionId);

        if (!attributeDefinition.IsSuccess)
        {
            return Result<CreateResponse>
                .Fail(attributeDefinition.Error);
        }

        var attributeExists = await _basketAttributeValidator
            .NotExistsAsync(attributeDefinition.Value!, request.Order);

        if (!attributeExists.IsSuccess)
        {
            return Result<CreateResponse>
                .Fail(attributeExists.Error);
        }


        var attribute = BasketAttributeCreater.Create(request);

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
                "Failed to create basket attribute. " +
                "AttributeDefinitionId: {AttributeDefinitionId}",
                request.AttributeDefinitionId);

            return Result<CreateResponse>.Fail(
                EntityErrorResources.BasketAttributeCreateFailed);
        }

        await _attributeInvalid.InvalidateCreateAsync(
            BasketAttributeCacheMapper.ToInfo(attribute));

        return Result<CreateResponse>.Success(
            new CreateResponse(
                attribute.Id,
                DateTime.UtcNow
        ));
    }
}