using ShagOxServer.Application.DTOs.Location.Cities.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Location;
public class RegionInvalidationService
    : ICacheInvalidationService<Region, long>
{

    private readonly ICityQueryRepository _cityRepository;
    private readonly CityInvalidationService _cityInvalid;

    //private readonly ICityTranslationQueryRepository _transRepository;
    //private readonly CityTranslationInvalidationService _transInvalid;

    private readonly ICacheService _cache;



    public RegionInvalidationService(
        ICityQueryRepository cityRepository,
        CityInvalidationService cityInvalid,
        //ICityTranslationQueryRepository transRepository,
        //CityTranslationInvalidationService transInvalid,
        ICacheService cache)
    {
        _cityRepository = cityRepository;
        _cityInvalid = cityInvalid;
        //_transRepository = transRepository;
        //_transInvalid = transInvalid;
        _cache = cache;
    }


    public async Task InvalidateDeleteAsync(
        long entityId)
    {
        await InvalidateAsync(entityId);


        var cityInfos =
            await GetCityInfos(entityId);

        foreach (var cityInfo in cityInfos)
        {
            await _cityInvalid
                .InvalidateDeleteAsync(cityInfo);
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
        long entityId)
    {
        await InvalidateAsync(entityId);


        var cityInfos =
           await GetCityInfos(entityId);

        foreach (var cityInfo in cityInfos)
        {
            await _cityInvalid
                .InvalidateUpdateAsync(cityInfo);
        }
    }

    private async Task InvalidateAsync(
        long entityId)
    {
        await _cache.RemoveByPatternAsync(CacheKeys
            .EntityLanguagePattern<City>(entityId));
    }

    private async Task<List<CityCacheInfo>> GetCityInfos(
        long entityId)
    {
        return await _cityRepository
            .GetCacheInfoByRegionAsync(entityId);
    }

    //private async Task<List<BaseTranslationCacheInfo>> GetTranslationInfos(
    //    long entityId)
    //{
    //    return await _transRepository
    //        .GetCacheInfosByTranslatableAsync(entityId);
    //}
}