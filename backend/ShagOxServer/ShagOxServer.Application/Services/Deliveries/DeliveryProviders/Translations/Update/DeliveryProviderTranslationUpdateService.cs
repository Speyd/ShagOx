using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Translations.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Translations.Delete;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Translations.Update;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Base.Translations.Query.Mapping;
using ShagOxServer.Application.Services.Caches.Invalidations.Deliveries.Translations;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Translations.Validator;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Validator;
using ShagOxServer.Domain.Entities.Deliveries;
using ShagOxServer.Domain.Entities.Deliveries.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Translations.Update;
public class DeliveryProviderTranslationUpdateService
    : BaseTranslationUpdateService<DeliveryProvider, 
        DeliveryProviderTranslation>,
    IDeliveryProviderTranslationUpdateService
{
    private readonly IRepository<DeliveryProviderTranslation> _deliveryRepository;
    private readonly DeliveryProviderTranslationValidator _deliveryTranslationValidator;

    private readonly DeliveryProviderTranslationInvalidationService _transInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeliveryProviderTranslationUpdateService> _logger;


    public DeliveryProviderTranslationUpdateService(
        IRepository<DeliveryProviderTranslation> deliveryRepository,
        IDeliveryProviderTranslationDeleteService deleteService,
        DeliveryProviderTranslationValidator deliveryTranslationValidator,
        DeliveryProviderValidator deliveryValidator,
        DeliveryProviderTranslationInvalidationService transInvalid,
        IUnitOfWork unitOfWork,
        ILogger<DeliveryProviderTranslationUpdateService> logger
    )
        : base(deliveryValidator, 
            deleteService,
            deliveryTranslationValidator, 
            logger, 
            unitOfWork)
    {
        _deliveryRepository = deliveryRepository;
        _deliveryTranslationValidator = deliveryTranslationValidator;
        _transInvalid = transInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        long deliveryTranslationId,
        DeliveryProviderTranslationUpdateRequest request,
        CancellationToken cancellationToken = default)
    {
        var delivery = await _deliveryTranslationValidator
            .GetByIdAsync(deliveryTranslationId, cancellationToken);

        if (!delivery.IsSuccess)
            return Result<UpdateResponse>.Fail(delivery.Error);


        var validation = await
             ValidateUpdatesAsync(delivery.Value!,
                request, cancellationToken);

        if (!validation.IsSuccess)
            return Result<UpdateResponse>.Fail(validation.Error);


        var updatedCount = DeliveryProviderTranslationUpdater
            .ApplyUpdates(delivery.Value!, request);

        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(result);

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            _deliveryRepository.Update(delivery.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogWarning(
                ex,
                "Concurrency conflict while updating delivery provider translation." +
                " Id: {Id}",
                deliveryTranslationId);

            return Result<UpdateResponse>.Fail(
                EntityErrorResources.ConcurrencyConflictMessage);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to update delivery provider translation. Id: {Id}",
                deliveryTranslationId);

            return Result<UpdateResponse>.Fail(
                EntityErrorResources.DeliveryProviderTranslationUpdateFailed);

        }

        await _transInvalid.InvalidateUpdateAsync(
            BaseTranslationCacheMapper.ToInfo(delivery.Value!));

        return Result<UpdateResponse>.Success(result);
    }
}
