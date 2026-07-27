using ShagOxServer.Application.DTOs.Advertisements.Favorites;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Query;
public interface IFavoriteQueryService
{
    Task<Result<FavoriteDto>> GetByIdAsync(
        int id);

    Task<Result<List<FavoriteDto>>> GetPagedAsync(
        PaginationParams pagination);

    Task<Result<int>> CountByAdvertisementAsync(
        int advertisementId);

    Task<Result<List<FavoriteDto>>> GetByUserAsync(
        int usderId,
        PaginationParams pagination);
}