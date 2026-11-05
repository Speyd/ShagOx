using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Update;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Update;
using ShagOxServer.Application.Services.Base.Update;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Update;

public partial class AdvertisementVariantUpdateService
    : BaseUpdateService<AdvertisementVariant>,
    IAdvertisementVariantUpdateService
{
    private async Task<Result<bool>> ValidateUpdateConflictsAsync(
        AdvertisementVariant variant,
        AdvertisementVariantUpdateRequest request,
        CancellationToken cancellationToken = default)
    {
        var attributes = request.Attributes ?? variant.Attributes;
        var advertId = request.AdvertisementId ?? variant.AdvertisementId;

        if (advertId != variant.AdvertisementId)
        {
            var advertExists = await _advertValidator
                .ExistsByIdAsync(advertId, cancellationToken);

            if (!advertExists.IsSuccess)
                return Result<bool>.Fail(advertExists.Error);
        }

        if (attributes != variant.Attributes ||
            advertId != variant.AdvertisementId)
        {
            var variantExists = await _variantValidator
                .NotExistsAsync(advertId, attributes, cancellationToken);

            if (!variantExists.IsSuccess)
                return Result<bool>.Fail(variantExists.Error);
        }

        if (request.Attributes is not null)
        {

            var isVariantExists = await _variantValidator
                .ValidateVariantAttributesAsync(
                    advertId,
                    request.Attributes);

            if (!isVariantExists.IsSuccess)
            {
                return Result<bool>
                    .Fail(isVariantExists.Error);
            }
        }


        return Result<bool>.Success(true);
    }

    protected override async Task<Result<bool>> ValidateUpdateConflictsAsync(
        AdvertisementVariant variant,
        CancellationToken cancellationToken = default)
    {
       var variantExists = await _variantValidator
            .NotExistsAsync(variant.AdvertisementId,
                variant.Attributes,
                cancellationToken);

       if (!variantExists.IsSuccess)
        return Result<bool>.Fail(variantExists.Error);

        return Result<bool>.Success(true);
    }
}
