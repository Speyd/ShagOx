using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.LineItems.BasketAttributes.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base.Delete;
using ShagOxServer.Application.Services.Caches.Invalidations.LineItems;
using ShagOxServer.Application.Services.LineItems.LineItemAttributes.Mapping;
using ShagOxServer.Application.Services.LineItems.LineItemAttributes.Validator;
using ShagOxServer.Domain.Entities.LineItems;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.LineItems.LineItemAttributes.Delete;
public class LineItemAttributeDeleteService
    : BaseDeleteService<LineItemAttribute>
    , ILineItemAttributeDeleteService
{
    private readonly IRepository<LineItemAttribute> _attributeRepository;
    private readonly LineItemAttributeValidator _attributeValidator;

    private readonly LineItemAttributeInvalidationService _attributeInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<LineItemAttributeDeleteService> _logger;


    public LineItemAttributeDeleteService(
        IRepository<LineItemAttribute> attributeRepository,
        LineItemAttributeValidator attributeValidator,
        LineItemAttributeInvalidationService attributeInvalid,
        IUnitOfWork unitOfWork,
        ILogger<LineItemAttributeDeleteService> logger
    )
        : base(attributeValidator, unitOfWork, logger)
    {
        _attributeRepository = attributeRepository;
        _attributeValidator = attributeValidator;
        _attributeInvalid = attributeInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        var attribute = await _attributeValidator
            .GetByIdIncludeAsync(id, cancellationToken);

        if (!attribute.IsSuccess)
            return Result<DeleteResponse>.Fail(attribute.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _attributeRepository.Delete(attribute.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to delete line item attribute. Id: {Id}",
                id);

            return Result<DeleteResponse>.Fail(
                EntityErrorResources.LineItemAttributeDeleteFailed);
        }

        await InvalidateCache(attribute.Value!);

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               attribute.Value!.Id,
               DateTime.UtcNow
           )
       );
    }

    public override async Task InvalidateCache(
        LineItemAttribute entity)
    {
        await _attributeInvalid.InvalidateDeleteAsync(
            LineItemAttributeCacheMapper.ToInfo(
                entity,
                entity.AttributeDefinition
                    .CategoryId)
            );
    }
}
