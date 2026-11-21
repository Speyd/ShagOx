using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Delete;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Update;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base.Update;
using ShagOxServer.Application.Services.Caches.Invalidations.Deliveries;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Mapping;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Validator;
using ShagOxServer.Domain.Entities.Deliveries;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Update;
public class DeliveryProviderUpdateService
    : BaseUpdateService<DeliveryProvider>,
    IDeliveryProviderUpdateService
{
    private readonly IRepository<DeliveryProvider> _deliveryRepository;
    private readonly DeliveryProviderValidator _deliveryValidator;

    private readonly DeliveryProviderInvalidationService _deliveryInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeliveryProviderUpdateService> _logger;


    public DeliveryProviderUpdateService(
        IRepository<DeliveryProvider> deliveryRepository,
        DeliveryProviderValidator deliveryValidator,
        IDeliveryProviderDeleteService deleteService,
        DeliveryProviderInvalidationService deliveryInvalid,
        IUnitOfWork unitOfWork,
        ILogger<DeliveryProviderUpdateService> logger
    )
        : base(deleteService, deliveryValidator, logger, unitOfWork)
    {
        _deliveryRepository = deliveryRepository;
        _deliveryValidator = deliveryValidator;
        _deliveryInvalid = deliveryInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        long deliveryId,
        DeliveryProviderUpdateRequest request,
        CancellationToken cancellationToken = default)
    {
        var delivery = await _deliveryValidator.
            GetByIdAsync(deliveryId, cancellationToken);
        if (!delivery.IsSuccess)
        {
            return Result<UpdateResponse>
                .Fail(delivery.Error);
        }

        var validation = await
             ValidateUpdatesAsync(delivery.Value!,
                request, cancellationToken);

        if (!validation.IsSuccess)
        {
            return Result<UpdateResponse>
                .Fail(validation.Error);
        }

        var updatedCount = DeliveryProviderUpdater
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
                "Concurrency conflict while updating delivery provider." +
                " Id: {Id}",
                deliveryId);

            return Result<UpdateResponse>.Fail(
                EntityErrorResources.ConcurrencyConflictMessage);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to update delivery provider. Id: {Id}",
                deliveryId);

            return Result<UpdateResponse>
                     .Fail(EntityErrorResources.DeliveryProviderUpdateFailed);
        }

        await _deliveryInvalid.InvalidateUpdateAsync(
            DeliveryProviderCacheMapper.ToInfo(delivery.Value!));

        return Result<UpdateResponse>.Success(result);
    }

    private async Task<Result<bool>> ValidateUpdatesAsync(
        DeliveryProvider delivery,
        DeliveryProviderUpdateRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request.Code is not null &&
           request.Code != delivery.Code)
        {
            var validator = await _deliveryValidator
                .NotExistsByCodeAsync(request.Code, cancellationToken);

            if (!validator.IsSuccess)
                return Result<bool>.Fail(validator.Error);
        }

        return Result<bool>.Success(true);
    }
}
