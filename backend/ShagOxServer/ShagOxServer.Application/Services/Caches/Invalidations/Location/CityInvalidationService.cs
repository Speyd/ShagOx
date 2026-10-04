using ShagOxServer.Application.DTOs.Auth.Users.Core.Cache;
using ShagOxServer.Application.DTOs.Location.Cities.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Auth;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Location;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Location;
public class CityInvalidationService
    : ICacheInvalidationService<City, CityCacheInfo>
{

    private readonly IUserQueryRepository _userRepository;
    private readonly UserInvalidationService _userInvalid;

    //private readonly ICategoryTranslationQueryRepository _transRepository;
    //private readonly CategoryTranslationInvalidationService _transInvalid;

    private readonly ICacheService _cache;



    public CityInvalidationService(
        IUserQueryRepository userRepository,
        UserInvalidationService userInvalid,
        //ICategoryTranslationQueryRepository transRepository,
        //CategoryTranslationInvalidationService transInvalid,
        ICacheService cache)
    {
        _userRepository = userRepository;
        _userInvalid = userInvalid;
        //_transRepository = transRepository;
        //_transInvalid = transInvalid;
        _cache = cache;
    }


    public async Task InvalidateDeleteAsync(
        CityCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);


        var userInfos =
            await GetUserInfos(entityInfo.Id);

        foreach (var userInfo in userInfos)
        {
            await _userInvalid
                .InvalidateDeleteAsync(userInfo);
        }

        //var transInfos =
        //   await GetTranslationInfos(entityInfo.Id);

        //foreach (var transInfo in transInfos)
        //{
        //    await _transInvalid
        //        .InvalidateDeleteAsync(transInfo);
        //}
    }

    public async Task InvalidateUpdateAsync(
        CityCacheInfo entityInfo)
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
        CityCacheInfo entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys
            .EntityLanguagePattern<City>(entityInfo.Id));

        await _cache.RemoveByPatternAsync(CityCache
            .ByRegionPattern(entityInfo.RegionId));
    }

    private async Task<List<UserCacheInfo>> GetUserInfos(
        long entityId)
    {
        return await _userRepository
            .GetCacheInfosByCityAsync(entityId);
    }

    //private async Task<List<BaseTranslationCacheInfo>> GetTranslationInfos(
    //    long entityId)
    //{
    //    return await _transRepository
    //        .GetCacheInfosByTranslatableAsync(entityId);
    //}
}