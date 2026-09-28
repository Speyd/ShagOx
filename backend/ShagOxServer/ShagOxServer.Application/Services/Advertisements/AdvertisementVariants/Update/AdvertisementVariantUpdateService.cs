using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Update;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Update;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Validator;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Update;
public class AdvertisementVariantUpdateService
    : IAdvertisementVariantUpdateService
{
    private readonly IRepository<AdvertisementVariant> _valueRepository;
    private readonly AdvertisementVariantValidator _valueValidator;

    private readonly AdvertisementValidator _advertValidator;


    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AdvertisementVariantUpdateService> _logger;


    public AdvertisementVariantUpdateService(
        IRepository<AdvertisementVariant> valueRepository,
        AdvertisementVariantValidator valueValidator,
        AdvertisementValidator advertValidator,
        IUnitOfWork unitOfWork,
        ILogger<AdvertisementVariantUpdateService> logger)
    {
        _valueRepository = valueRepository;
        _valueValidator = valueValidator;
        _advertValidator = advertValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
     long valueId,
     AdvertisementVariantUpdateRequest request)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var result = await UpdateInternalAsync(valueId, request);

            if (!result.IsSuccess)
            {
                await _unitOfWork.RollbackAsync();
                return result;
            }

            await _unitOfWork.CommitAsync();

            return result;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to update advertisement variant. Id: {Id}",
                valueId);

            return Result<UpdateResponse>.Fail(
                EntityErrorResources.AdvertisementVariantUpdateFailed);
        }
    }

    public async Task<Result<UpdateResponse>> UpdateInternalAsync(
        long valueId,
        AdvertisementVariantUpdateRequest request)
    {
        var value = await _valueValidator.GetByIdAsync(valueId);

        if (!value.IsSuccess)
        {
            return Result<UpdateResponse>
                .Fail(value.Error);
        }

        var validation = await ValidateUpdatesAsync(
            value.Value!,
            request);

        if (!validation.IsSuccess)
        {
            return Result<UpdateResponse>
                .Fail(validation.Error);
        }

        var updatedCount = AdvertisementVariantUpdater
            .ApplyUpdates(value.Value!, request);

        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow);

        if (updatedCount == 0)
        {
            return Result<UpdateResponse>.Success(result);
        }

        _valueRepository.Update(value.Value!);

        return Result<UpdateResponse>.Success(result);
    }

    private async Task<Result<bool>> ValidateUpdatesAsync(
        AdvertisementVariant variant,
        AdvertisementVariantUpdateRequest request)
    {
        var attributes = request.Attributes ?? variant.Attributes;
        var advertId = request.AdvertisementId ?? variant.AdvertisementId;

        if (advertId != variant.AdvertisementId)
        {
            var advertExists = await _advertValidator
                .ExistsByIdAsync(advertId);

            if (!advertExists.IsSuccess)
                return Result<bool>.Fail(advertExists.Error);
        }

        if (attributes != variant.Attributes ||
            advertId != variant.AdvertisementId)
        {
            var variantExists = await _valueValidator
                .NotExistsAsync(advertId, attributes);

            if (!variantExists.IsSuccess)
                return Result<bool>.Fail(variantExists.Error);
        }

        return Result<bool>.Success(true);
    }
}