using ShagOxServer.Application.DTOs.Advertisements.Favorites;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Advertisements.Favorites.Query;
public interface IFavoriteQueryService
{
    Task<Result<FavoriteDto>> GetByIdAsync(
        int id);

    Task<Result<int>> CountByAdvertisementIdAsync(
        int advertisementId);

    Task<Result<List<FavoriteDto>>> GetByUserIdAsync(
        int usderId,
        PaginationParams pagination);
}