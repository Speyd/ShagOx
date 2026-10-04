using ShagOxServer.Application.DTOs.Advertisements.Favorites.Query;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Query;
public interface IFavoriteQueryService
    : IQueryService<FavoriteDto, Favorite, FavoriteSearchFilter>
{
    Task<Result<int>> CountByAdvertisementAsync(
        long advertisementId);

    Task<Result<PagedResult<FavoriteDto>>> GetByUserAsync(
        long usderId,
        PaginationParams pagination);
}