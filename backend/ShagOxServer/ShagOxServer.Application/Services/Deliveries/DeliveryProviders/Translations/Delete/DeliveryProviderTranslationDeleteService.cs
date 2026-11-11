using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Translations.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Base.Delete;
using ShagOxServer.Application.Services.Base.Translations.Query.Mapping;
using ShagOxServer.Application.Services.Caches.Invalidations.Deliveries.Translations;
using ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Translations.Validator;
using ShagOxServer.Domain.Entities.Deliveries.Translations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Deliveries.DeliveryProviders.Translations.Delete;
public class DeliveryProviderTranslationDeleteService
    : BaseDeleteService<DeliveryProviderTranslation>,
    IDeliveryProviderTranslationDeleteService
{
    private readonly IRepository<DeliveryProviderTranslation> _deliveryRepository;
    private readonly DeliveryProviderTranslationValidator _deliveryValidator;

    private readonly DeliveryProviderTranslationInvalidationService _transInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeliveryProviderTranslationDeleteService> _logger;

    public DeliveryProviderTranslationDeleteService(
        IRepository<DeliveryProviderTranslation> deliveryRepository,
        DeliveryProviderTranslationValidator deliveryValidator,
        DeliveryProviderTranslationInvalidationService transInvalid,
        IUnitOfWork unitOfWork,
        ILogger<DeliveryProviderTranslationDeleteService> logger
    )
        : base(deliveryValidator, unitOfWork, logger)
    {
        _deliveryRepository = deliveryRepository;
        _deliveryValidator = deliveryValidator;
        _transInvalid = transInvalid;
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
                "Failed to delete delivery provider translation. Id: {Id}",
                id);

            return Result<DeleteResponse>
                .Fail(EntityErrorResources.DeliveryProviderTranslationDeleteFailed);
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
        DeliveryProviderTranslation entity)
    {
        await _transInvalid.InvalidateDeleteAsync(
            BaseTranslationCacheMapper.ToInfo(entity));
    }
}
