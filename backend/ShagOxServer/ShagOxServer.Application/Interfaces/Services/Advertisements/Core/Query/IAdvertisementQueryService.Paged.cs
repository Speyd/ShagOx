using ShagOxServer.Application.DTOs.Advertisements.Core.Query.Catalog;
using ShagOxServer.Application.DTOs.Advertisements.Core.Query.Core;
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
    Task<Result<PagedResult<AdvertisementShortDto>>> GetShortPagedAsync(
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<Result<PagedResult<AdvertisementCatalogDto>>> GetCatalogPagedAsync(
        long? userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}
