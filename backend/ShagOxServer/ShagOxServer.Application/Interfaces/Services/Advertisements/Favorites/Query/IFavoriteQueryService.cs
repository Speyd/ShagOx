using ShagOxServer.Application.DTOs.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Query;
public interface IFavoriteQueryService
    : IQueryService<FavoriteDto>
{
    Task<Result<int>> CountByAdvertisementAsync(
        int advertisementId);

    Task<Result<PagedResult<FavoriteDto>>> GetByUserAsync(
        int usderId,
        PaginationParams pagination);
}