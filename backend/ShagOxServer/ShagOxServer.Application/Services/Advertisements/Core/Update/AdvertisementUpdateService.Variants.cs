using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Update;
using ShagOxServer.Application.Resources.Validations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System.Text.Json;

namespace ShagOxServer.Application.Services.Advertisements.Core.Update;
public partial class AdvertisementUpdateService
{
    private Result<Dictionary<long, AdvertisementVariantUpdateRequest>?>
        ParseAndValidateVariants(string? variantsJson)
    {
        if (string.IsNullOrWhiteSpace(variantsJson))
        {
            return Result<Dictionary<long,
                    AdvertisementVariantUpdateRequest>?>
                .Success(null);
        }

        try
        {
            var variants = JsonSerializer.Deserialize<
                Dictionary<long, AdvertisementVariantUpdateRequest>
                    >(variantsJson);

            if (variants is null)
            {
                return Result<Dictionary<long,
                        AdvertisementVariantUpdateRequest>?>
                    .Success(null);
            }

            var duplicateExists = _variantValidator
                .ValidateUniqueAttributes(
                    variants.Values
                        .Select(x => x.Attributes)
                        .ToList()
                );

            if (!duplicateExists.IsSuccess)
            {
                return Result<Dictionary<long,
                        AdvertisementVariantUpdateRequest>?>
                    .Fail(duplicateExists.Error!);
            }

            return Result<Dictionary<long,
                    AdvertisementVariantUpdateRequest>?>
                .Success(variants);
        }
        catch (JsonException)
        {
            return Result<Dictionary<long, AdvertisementVariantUpdateRequest>?>
                .Fail(ValidationResources.InvalidJson);
        }
    }
}