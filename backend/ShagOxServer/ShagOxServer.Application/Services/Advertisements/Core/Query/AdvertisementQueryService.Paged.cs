using ShagOxServer.Application.DTOs.Advertisements.Core.Query.Catalog;
using ShagOxServer.Application.DTOs.Advertisements.Core.Query.Core;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Query;
using ShagOxServer.Application.Services.Advertisements.Core.Mapping.Catalog;
using ShagOxServer.Application.Services.Advertisements.Core.Mapping.Core;
using ShagOxServer.Application.Services.Base.Localized;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Advertisements.Core.Query;

public partial class AdvertisementQueryService
    : BaseLocalizedQueryService<
        AdvertisementDto,
        Advertisement,
        AdvertisementSearchFilter>,
      IAdvertisementQueryService
{
    public async Task<Result<PagedResult<AdvertisementShortDto>>> GetShortPagedAsync(
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var adverts = await _advertisementRepository
            .GetPagedAsync(pagination,
                cancellationToken, status);

        return await adverts.ToResultPagedAsync(async advert =>
        {
            var convertedPrices = await ConvertPricesAsync(
                advert,
                cancellationToken);

            var statusDto = await _statusService
                .ApplyMapperAsync(advert.Status);

            return AdvertisementShortMapper.ToDto(
                advert,
                convertedPrices,
                statusDto);
        });
    }

    public async Task<Result<PagedResult<AdvertisementCatalogDto>>> GetCatalogPagedAsync(
        long? userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var adverts = await _advertisementRepository
            .GetCatalogPagedAsync(userId, pagination,
                cancellationToken, status);

        return await adverts.ToResultPagedAsync(async advert =>
        {
            var convertedPrices = await ConvertPricesAsync(
                advert.Advertisement,
                cancellationToken);

            var variants = await LoadVariantDtosAsync(
                advert.Advertisement,
                cancellationToken);

            var attributeDto = await _attributeProcessor
                .GetAttributesAsync(
                     advert.Advertisement.Attributes,
                    advert.Advertisement.CategoryId
                );

            return AdvertisementRatingMapper.ToDto(
                advert,
                convertedPrices,
                variants,
                attributeDto.Value!);
        });
    }
}
