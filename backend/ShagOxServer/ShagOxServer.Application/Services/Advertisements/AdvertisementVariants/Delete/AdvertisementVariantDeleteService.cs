using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Validator;
using ShagOxServer.Application.Services.Base.Delete;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Delete;

public class AdvertisementVariantDeleteService
    : BaseDeleteService<AdvertisementVariant>
    , IAdvertisementVariantDeleteService
{
    private readonly IRepository<AdvertisementVariant> _variantRepository;
    private readonly IAdvertisementVariantCommandsRepository _variantCommand;
    private readonly AdvertisementVariantValidator _variantValidator;

    private readonly AdvertisementVariantInvalidationService _variantInvalid;

    private readonly IAdvertisementQueryRepository _advertRepository;
    private readonly AdvertisementInvalidationService _advertInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AdvertisementVariantDeleteService> _logger;


    public AdvertisementVariantDeleteService(
        IRepository<AdvertisementVariant> variantRepository,
        ICommandRepository<AdvertisementVariant> commandRepository,
        IAdvertisementVariantCommandsRepository variantCommand,
        AdvertisementVariantValidator variantValidator,
        AdvertisementVariantInvalidationService variantInvalid,
        IAdvertisementQueryRepository advertRepository,
        AdvertisementInvalidationService advertInvalid,
        IUnitOfWork unitOfWork,
        ILogger<AdvertisementVariantDeleteService> logger
    )
        : base(commandRepository, variantValidator, unitOfWork, logger)
    {
        _variantRepository = variantRepository;
        _variantCommand = variantCommand;
        _variantValidator = variantValidator;
        _variantInvalid = variantInvalid;
        _advertRepository = advertRepository;
        _advertInvalid = advertInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        var variant = await _variantValidator
            .GetByIdAsync(id, cancellationToken);

        if (!variant.IsSuccess)
            return Result<DeleteResponse>
                .Fail(variant.Error);

        // Позиції замовлень посилаються на варіант з Restrict, тому варіант,
        // який уже купували, фізично видалити не можна: видаляємо м'яко
        // і прибираємо з кошиків, щоб його не можна було замовити знову.
        var hasOrders = await _variantCommand
            .HasOrderItemsAsync(id, cancellationToken);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            if (hasOrders)
            {
                await _variantCommand
                    .RemoveFromBasketsAsync(id, cancellationToken);

                await _repository
                    .SoftDeleteAsync(id, cancellationToken);
            }
            else
            {
                var result = await DeleteInternalAsync(
                    variant.Value!);

                if (!result.IsSuccess)
                {
                    await _unitOfWork.RollbackAsync();

                    return result;
                }
            }

            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to delete advertisement variant. Id: {Id}",
                id);

            return Result<DeleteResponse>
                .Fail(
                    EntityErrorResources
                        .AdvertisementVariantDeleteFailed);
        }

        if (hasOrders)
        {
            await _variantInvalid.InvalidateDeleteAsync(
                variant.Value!.GetCacheContext());
        }

        await InvalidateAdvertisementAsync(
            variant.Value!.AdvertisementId, cancellationToken);

        return Result<DeleteResponse>.Success(
            new DeleteResponse(
                id,
                DateTime.UtcNow));
    }


    public async Task<Result<DeleteResponse>> DeleteInternalAsync(
        AdvertisementVariant variant)
    {
        _variantRepository.Delete(variant);

        return Result<DeleteResponse>.Success(
            new DeleteResponse(
                variant.Id,
                DateTime.UtcNow));
    }

    /// <summary>
    /// Оголошення кешується разом зі списком варіантів, тож після видалення
    /// варіанта кеш оголошення теж треба скинути.
    /// </summary>
    private async Task InvalidateAdvertisementAsync(
        long advertId,
        CancellationToken cancellationToken)
    {
        var advertInfo = await _advertRepository
            .GetCacheInfoByIdAsync(advertId,
                cancellationToken, EntityStatus.All);

        if (advertInfo is not null)
            await _advertInvalid.InvalidateUpdateAsync(advertInfo);
    }
}
