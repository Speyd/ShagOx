using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base.Delete;
using ShagOxServer.Application.Services.Caches.Invalidations.Deliveries;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Mapping;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Validator;
using ShagOxServer.Domain.Entities.Deliveries;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Delete;
public class DeliveryProviderDeleteService
    : BaseDeleteService<DeliveryProvider>
    , IDeliveryProviderDeleteService
{
    private readonly IRepository<DeliveryProvider> _deliveryRepository;
    private readonly DeliveryProviderValidator _deliveryValidator;

    private readonly DeliveryProviderInvalidationService _deliveryInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeliveryProviderDeleteService> _logger;


    public DeliveryProviderDeleteService(
        IRepository<DeliveryProvider> deliveryRepository,
        DeliveryProviderValidator deliveryValidator,
        DeliveryProviderInvalidationService deliveryInvalid,
        IUnitOfWork unitOfWork,
        ILogger<DeliveryProviderDeleteService> logger
    )
        : base(deliveryValidator, unitOfWork, logger)
    {
        _deliveryRepository = deliveryRepository;
        _deliveryValidator = deliveryValidator;
        _deliveryInvalid = deliveryInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        var delivery = await _deliveryValidator
            .GetByIdAsync(id, cancellationToken);

        if (!delivery.IsSuccess)
            return Result<DeleteResponse>.Fail(delivery.Error);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _deliveryRepository.Delete(delivery.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to delete delivery provider. Id: {Id}",
                id);

            return Result<DeleteResponse>
                     .Fail(EntityErrorResources.DeliveryProviderDeleteFailed);
        }

        await InvalidateCache(delivery.Value!);

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               delivery.Value!.Id,
               DateTime.UtcNow
           )
       );
    }

    public override async Task InvalidateCache(
        DeliveryProvider entity)
    {
        await _deliveryInvalid.InvalidateDeleteAsync(
           DeliveryProviderCacheMapper.ToInfo(entity));
    }
}
