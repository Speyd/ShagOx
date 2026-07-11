using ShagOxServer.Application.DTOs.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Query;
using ShagOxServer.Application.Services.Advertisements.Favorites.Mapping;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Query;
public class FavoriteQueryService : IFavoriteQueryService
{
    private readonly IFavoriteQueryRepository _repository;


    public FavoriteQueryService(
        IFavoriteQueryRepository favoriteRepository)
    {
        _repository = favoriteRepository;
    }

    public async Task<Result<FavoriteDto>> GetByIdAsync(
        int id)
    {
        var favorite = await _repository.GetByIdAsync(id);

        return favorite.ToResult(FavoriteMapper.ToDto);
    }

    public async Task<Result<int>> CountByAdvertisementIdAsync(
        int advertisementId)
    {
        var count = await _repository.CountByAdvertisementIdAsync(advertisementId);

        return Result<int>.Success(count);
    }

    public async Task<Result<List<FavoriteDto>>> GetByUserIdAsync(
        int usderId,
        PaginationParams pagination)
    {
        var favorites = await _repository.GetByUserIdAsync(usderId, pagination);

        return favorites.ToResultList(FavoriteMapper.ToDto);
    }
}