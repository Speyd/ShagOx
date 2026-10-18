using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Create;
using ShagOxServer.Application.Resources.Validations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ShagOxServer.Application.Services.Advertisements.Core.Create;
public partial class AdvertisementCreateService
{
    private Result<List<AdvertisementVariantCreateRequest>?> ParseAndValidateVariants(
        string? variantsJson)
    {
        if (string.IsNullOrWhiteSpace(variantsJson))
        {
            return Result<List<AdvertisementVariantCreateRequest>?>
                .Success(null);
        }

        try
        {
            if (string.IsNullOrWhiteSpace(variantsJson))
            {
                return Result<List<AdvertisementVariantCreateRequest>?>
                    .Success(null);
            }

            var variants = JsonSerializer.Deserialize<
                List<AdvertisementVariantCreateRequest>
                    >(variantsJson);

            if (variants is null)
            {
                return Result<List<AdvertisementVariantCreateRequest>?>
                    .Success(null);
            }

            var duplicateExists = _variantValidator
                .ValidateUniqueAttributes(
                    variants
                        .Select(x => x.Attributes)
                        .ToList()
                );

            if (!duplicateExists.IsSuccess)
            {
                return Result<List<AdvertisementVariantCreateRequest>?>
                    .Fail(duplicateExists.Error!);
            }

            return Result<List<AdvertisementVariantCreateRequest>?>
                .Success(variants);
        }
        catch (JsonException)
        {
            return Result<List<AdvertisementVariantCreateRequest>?>
                .Fail(ValidationResources.InvalidJson);
        }
    }

    private async Task<Result<bool>> CreateVariantsAsync(
        long userId,
        long advertisementId,
        IEnumerable<AdvertisementVariantCreateRequest>? variants)
    {
        if (variants is null)
            return Result<bool>.Success(true);

        foreach (var variant in variants)
        {
            var newVariant = variant with
            {
                AdvertisementId = advertisementId
            };

            var result = await _variantCreateService
                .CreateInternalAsync(newVariant);

            if (!result.IsSuccess)
            {
                _logger.LogError(
                    "Failed to create advertisement(variant create). " +
                    "UserId: {UserId}",
                    userId);

                return Result<bool>.Fail(result.Error);
            }
        }

        return Result<bool>.Success(true);
    }
}
