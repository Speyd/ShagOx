using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Advertisements.Favorites.Query;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Common.Currencies;
using ShagOxServer.Application.Services.Advertisements.Favorites.Mapping;
using ShagOxServer.Application.Services.Base.Query;
using ShagOxServer.Domain.Base.Enum;
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

    protected readonly IAdvertisementQueryService _advertService;
    protected readonly IStatusQueryService _statusService;

    protected override bool CacheBySearch => false;

    protected override bool CacheByPaged => false;


    public FavoriteQueryService(
        IFavoriteQueryRepository favoriteRepository,
        IUserQueryService userService,
        IAdvertisementQueryService advertService,
        IStatusQueryService statusService,
        IDataCacheService cacheService,
        IOptions<CacheSettings> settings
    )
        : base(favoriteRepository, cacheService, settings)
    {
        _favoriteRepository = favoriteRepository;
        _userService = userService;
        _advertService = advertService;
        _statusService = statusService;
    }


    public override async Task<FavoriteDto> ApplyMapperAsync(
        Favorite entity)
    {
        var userDto = await _userService
            .ApplyMapperAsync(entity.Owner);

        var converts = await _advertService.ConvertPricesAsync(
           entity.Advertisement);

        var statusDto = await _statusService
            .ApplyMapperAsync(entity.Advertisement.Status);

        return FavoriteMapper.ToDto(
            entity,
            userDto,
            converts,
            statusDto);
    }

    public async Task<Result<int>> CountByAdvertisementAsync(
        long advertisementId,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var count = await _favoriteRepository
            .CountByAdvertisementAsync(advertisementId,
                cancellationToken, status);

        return Result<int>.Success(count);
    }

    public async Task<Result<PagedResult<FavoriteDto>>> GetByUserAsync(
        long userId,
        PaginationParams pagination,
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        var cacheKey = FavoriteCache.ByUser(
            userId,
            pagination.Page,
            pagination.PageSize, status);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var favorites = await _favoriteRepository
                    .GetByUserAsync(userId, pagination, 
                        cancellationToken, status);

                return await favorites.ToResultPagedAsync(
                    ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }
}
