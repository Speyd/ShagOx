using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Advertisements.Favorites.Mapping;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Caches.Advertisements;
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
    private readonly IUserQueryService _userService;



    public FavoriteQueryService(
        IFavoriteQueryRepository favoriteRepository,
        IUserQueryService userService,
        ICacheService cacheService,
        IOptions<CacheSettings> settings
    )
        : base(favoriteRepository, cacheService, settings)
    {
        _favoriteRepository = favoriteRepository;
        _userService = userService;
    }


    public override async Task<FavoriteDto> ApplyMapperAsync(
        Favorite entity)
    {
        var userDto = await _userService
            .ApplyMapperAsync(entity.User);

        return FavoriteMapper.ToDto(entity, userDto);
    }

    public async Task<Result<int>> CountByAdvertisementAsync(
        long advertisementId)
    {
        var count = await _favoriteRepository
            .CountByAdvertisementAsync(advertisementId);

        return Result<int>.Success(count);
    }

    public async Task<Result<PagedResult<FavoriteDto>>> GetByUserAsync(
        long userId,
        PaginationParams pagination)
    {
        var cacheKey = FavoriteCache.ByUser(
            userId,
            pagination.Page,
            pagination.PageSize);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var favorites = await _favoriteRepository
                    .GetByUserAsync(userId, pagination);

                return await favorites.ToResultPagedAsync(
                    ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }
}