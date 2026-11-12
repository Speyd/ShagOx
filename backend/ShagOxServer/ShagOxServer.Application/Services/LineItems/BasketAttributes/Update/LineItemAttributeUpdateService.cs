using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.LineItems.LineItemAttributes.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.LineItems.BasketAttributes.Delete;
using ShagOxServer.Application.Interfaces.Services.LineItems.BasketAttributes.Update;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base.Update;
using ShagOxServer.Application.Services.Caches.Invalidations.LineItems;
using ShagOxServer.Application.Services.Dictionaries.Attributes.AttributeDefinitions.Validator;
using ShagOxServer.Application.Services.LineItems.LineItemAttributes.Validator;
using ShagOxServer.Application.Services.LineItems.LineItemAttributes.Mapping;
using ShagOxServer.Application.Services.LineItems.LineItemAttributes.Update.Validator;
using ShagOxServer.Domain.Entities.LineItems;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.LineItems.LineItemAttributes.Update;
public class LineItemAttributeUpdateService
    : BaseUpdateService<LineItemAttribute>,
    ILineItemAttributeUpdateService
{
    private readonly IRepository<LineItemAttribute> _attributeRepository;
    private readonly LineItemAttributeValidator _attributeValidator;
    private readonly LineItemAttributeUpdateValidator _attributeUpdateValidator;

    private readonly AttributeDefinitionValidator _attributeDefinitionValidator;

    private readonly LineItemAttributeInvalidationService _attributeInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<LineItemAttributeUpdateService> _logger;


    public LineItemAttributeUpdateService(
        IRepository<LineItemAttribute> attributeRepository,
        LineItemAttributeValidator attributeValidator,
        ILineItemAttributeDeleteService attributeDeleteService,
        LineItemAttributeUpdateValidator attributeUpdateValidator,
        AttributeDefinitionValidator attributeDefinitionValidator,
        LineItemAttributeInvalidationService attributeInvalid,
        IUnitOfWork unitOfWork,
        ILogger<LineItemAttributeUpdateService> logger
    )
        : base(attributeDeleteService, attributeValidator, logger, unitOfWork)
    {
        _attributeRepository = attributeRepository;
        _attributeValidator = attributeValidator;
        _attributeUpdateValidator = attributeUpdateValidator;
        _attributeDefinitionValidator = attributeDefinitionValidator;
        _attributeInvalid = attributeInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        long attributeId,
        LineItemAttributeUpdateRequest request,
        CancellationToken cancellationToken = default)
    {
        var attribute = await _attributeValidator
            .GetByIdIncludeAsync(attributeId, cancellationToken);

        if (!attribute.IsSuccess)
            return Result<UpdateResponse>.Fail(attribute.Error);


        var changeValidator = _attributeUpdateValidator
            .HasChangesValidator(attribute.Value!, request);

        if (!changeValidator.IsSuccess)
        {
            return Result<UpdateResponse>.Success(
                new UpdateResponse(
                    0,
                    DateTime.UtcNow
            ));
        }

        var validation = await
             ValidateUpdatesAsync(changeValidator.Value!, 
                request, cancellationToken);

        if (!validation.IsSuccess)
            return Result<UpdateResponse>.Fail(validation.Error);


        var updatedCount = LineItemAttributeUpdater
            .ApplyUpdates(attribute.Value!, request);

        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(result);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _attributeRepository.Update(attribute.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogWarning(
                ex,
                "Concurrency conflict while updating basket attribute." +
                " Id: {Id}",
                attributeId);

            return Result<UpdateResponse>.Fail(
                EntityErrorResources.ConcurrencyConflictMessage);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to update line item attribute. " +
                "Id: {Id}, AttributeDefinitionId: {AttributeDefinitionId}",
                attributeId,
                request.AttributeDefinitionId);

            return Result<UpdateResponse>.Fail(
                EntityErrorResources.LineItemAttributeUpdateFailed);
        }

        await _attributeInvalid.InvalidateUpdateAsync(
            LineItemAttributeCacheMapper.ToInfo(
                attribute.Value!,
                attribute.Value!.AttributeDefinition.CategoryId)
            );

        return Result<UpdateResponse>.Success(result);
    }

    private async Task<Result<bool>> ValidateUpdatesAsync(
        (int order, long attributeDefinitionId) changeValidator,
        LineItemAttributeUpdateRequest request,
        CancellationToken cancellationToken = default)
    {
        var attributeDef = await _attributeDefinitionValidator
            .GetByIdAsync(changeValidator.attributeDefinitionId, 
                cancellationToken);

        if (!attributeDef.IsSuccess)
            return Result<bool>.Fail(attributeDef.Error);

        var existsValidator = await _attributeValidator
            .NotExistsAsync(attributeDef.Value!,
                changeValidator.order, cancellationToken);

        return existsValidator;
    }
}
