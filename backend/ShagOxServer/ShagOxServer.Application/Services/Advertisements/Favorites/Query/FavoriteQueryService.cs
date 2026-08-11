using ShagOxServer.Application.DTOs.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Query;
using ShagOxServer.Application.Services.Advertisements.Favorites.Mapping;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Query;
public class FavoriteQueryService 
    : IFavoriteQueryService
{
    private readonly IFavoriteQueryRepository _favoriteRepository;


    public FavoriteQueryService(
        IFavoriteQueryRepository favoriteRepository)
    {
        _favoriteRepository = favoriteRepository;
    }


    public async Task<Result<FavoriteDto>> GetByIdAsync(
        int id)
    {
        var favorite = await _favoriteRepository
            .GetByIdAsync(id);

        return favorite.ToResult(FavoriteMapper.ToDto);
    }

    public async Task<Result<PagedResult<FavoriteDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var favorits = await _favoriteRepository
            .GetPagedAsync(pagination);

        return favorits.ToResultPaged(FavoriteMapper.ToDto);
    }

    public async Task<Result<int>> CountByAdvertisementAsync(
        int advertisementId)
    {
        var count = await _favoriteRepository
            .CountByAdvertisementAsync(advertisementId);

        return Result<int>.Success(count);
    }

    public async Task<Result<PagedResult<FavoriteDto>>> GetByUserAsync(
        int usderId,
        PaginationParams pagination)
    {
        var favorites = await _favoriteRepository
            .GetByUserAsync(usderId, pagination);

        return favorites.ToResultPaged(FavoriteMapper.ToDto);
    }
}