using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Auth.UserInvalid;
using ShagOxServer.Application.Services.Caches.Invalidations.Location.Translation;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Location;
using ShagOxServer.Domain.Cache.Auth.User;
using ShagOxServer.Domain.Cache.Location;
using ShagOxServer.Domain.Cache.Translations;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Location;
public class CityInvalidationService
    : ICacheInvalidationService<City, CityCacheContext>
{

    private readonly IUserQueryRepository _userRepository;
    private readonly UserInvalidationService _userInvalid;

    private readonly ICityTranslationQueryRepository _transRepository;
    private readonly CityTranslationInvalidationService _transInvalid;

    private readonly IDataCacheService _cache;



    public CityInvalidationService(
        IUserQueryRepository userRepository,
        UserInvalidationService userInvalid,
        ICityTranslationQueryRepository transRepository,
        CityTranslationInvalidationService transInvalid,
        IDataCacheService cache)
    {
        _userRepository = userRepository;
        _userInvalid = userInvalid;
        _transRepository = transRepository;
        _transInvalid = transInvalid;
        _cache = cache;
    }


    public async Task InvalidateCreateAsync(
       CityCacheContext entityInfo)
    {
        await InvalidatePatternAsync(entityInfo);
    }

    public async Task InvalidateDeleteAsync(
        CityCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);


        var userInfos =
            await GetUserInfos(entityInfo.Id);

        foreach (var userInfo in userInfos)
        {
            await _userInvalid
                .InvalidateDeleteAsync(userInfo);
        }

        var transInfos =
           await GetTranslationInfos(entityInfo.Id);

        foreach (var transInfo in transInfos)
        {
            await _transInvalid
                .InvalidateDeleteAsync(transInfo);
        }
    }

    public async Task InvalidateUpdateAsync(
        CityCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);


        var userInfos =
           await GetUserInfos(entityInfo.Id);

        foreach (var userInfo in userInfos)
        {
            await _userInvalid
                .InvalidateUpdateAsync(userInfo);
        }
    }

    private async Task InvalidateAsync(
        CityCacheContext entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys
            .EntityLanguagePattern<City>(entityInfo.Id));

        await InvalidatePatternAsync(entityInfo);
    }

    private async Task InvalidatePatternAsync(
        CityCacheContext entityInfo)
    {
        await _cache.RemoveByPatternAsync(CityCache
            .ByRegionPattern(entityInfo.RegionId));

        await _cache.RemoveByPatternAsync(CacheKeys.
            EntityLanguagePagedPattern<City>(
                entityInfo.Status));

        await _cache.RemoveByPatternAsync(CacheKeys.
             LanguageSearchPattern<City>(
                entityInfo.Status));
    }

    private async Task<List<UserCacheContext>> GetUserInfos(
        long entityId)
    {
        return await _userRepository
            .GetCacheInfosByCityAsync(entityId);
    }

    private async Task<List<TranslationCacheContext>> GetTranslationInfos(
        long entityId)
    {
        return await _transRepository
            .GetCacheInfosByTranslatableAsync(entityId);
    }
}
