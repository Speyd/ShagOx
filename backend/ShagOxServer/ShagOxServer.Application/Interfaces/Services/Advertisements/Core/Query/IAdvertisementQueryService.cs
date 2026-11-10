using ShagOxServer.Application.DTOs.Advertisements.Core.Query.Core;
using ShagOxServer.Application.DTOs.Common.Currencies;
using ShagOxServer.Application.Interfaces.Services.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Query;
public partial interface IAdvertisementQueryService
    : IQueryService<AdvertisementDto, Advertisement, AdvertisementSearchFilter>
{
    Task<Result<PagedResult<AdvertisementDto>>> GetBySellerAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<Result<PagedResult<AdvertisementDto>>> GetPurchasedByUserAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<ConvertedPricesDto> ConvertPricesAsync(
        Advertisement advert,
        CancellationToken cancellationToken = default);
}
