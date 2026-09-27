using ShagOxServer.Application.DTOs.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Query;
using ShagOxServer.Application.Services.Advertisements.Favorites.Mapping;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Advertisements.Favorites.Query;
public class FavoriteQueryService 
    : BaseQueryService<
        FavoriteDto,
        Favorite,
        FavoriteSearchFilter
        >,
    IFavoriteQueryService
{
    private readonly IFavoriteQueryRepository _favoriteRepository;


    public FavoriteQueryService(
        IFavoriteQueryRepository favoriteRepository
    )
        : base(favoriteRepository)
    {
        _favoriteRepository = favoriteRepository;
    }


    protected override FavoriteDto ApplyMapper(
        Favorite entity)
    {
        return FavoriteMapper.ToDto(entity);
    }

    public async Task<Result<int>> CountByAdvertisementAsync(
        long advertisementId)
    {
        var count = await _favoriteRepository
            .CountByAdvertisementAsync(advertisementId);

        return Result<int>.Success(count);
    }

    public async Task<Result<PagedResult<FavoriteDto>>> GetByUserAsync(
        long usderId,
        PaginationParams pagination)
    {
        var favorites = await _favoriteRepository
            .GetByUserAsync(usderId, pagination);

        return favorites.ToResultPaged(FavoriteMapper.ToDto);
    }
}