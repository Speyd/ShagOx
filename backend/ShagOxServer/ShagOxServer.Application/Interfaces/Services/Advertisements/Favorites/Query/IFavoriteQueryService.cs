using ShagOxServer.Application.DTOs.Advertisements.Favorites.Query;
using ShagOxServer.Application.Interfaces.Services.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Query;
public interface IFavoriteQueryService
    : IQueryService<FavoriteDto, Favorite, FavoriteSearchFilter>
{
    Task<Result<int>> CountByAdvertisementAsync(
        long advertisementId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);

    Task<Result<PagedResult<FavoriteDto>>> GetByUserAsync(
        long usderId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active);
}
