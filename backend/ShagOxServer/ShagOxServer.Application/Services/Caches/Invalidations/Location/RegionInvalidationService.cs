using ShagOxServer.Application.Interfaces.Repositories.Location.Cities.Query;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Location.Translation;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Cache.Location;
using ShagOxServer.Domain.Cache.Translations;
using ShagOxServer.Domain.Entities.Location;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Location;
public class RegionInvalidationService
    : ICacheInvalidationService<Region, RegionCacheContext>
{

    private readonly ICityQueryRepository _cityRepository;
    private readonly CityInvalidationService _cityInvalid;

    private readonly IRegionTranslationQueryRepository _transRepository;
    private readonly RegionTranslationInvalidationService _transInvalid;

    private readonly IDataCacheService _cache;



    public RegionInvalidationService(
        ICityQueryRepository cityRepository,
        CityInvalidationService cityInvalid,
        IRegionTranslationQueryRepository transRepository,
        RegionTranslationInvalidationService transInvalid,
        IDataCacheService cache)
    {
        _cityRepository = cityRepository;
        _cityInvalid = cityInvalid;
        _transRepository = transRepository;
        _transInvalid = transInvalid;
        _cache = cache;
    }


    public async Task InvalidateCreateAsync(
       RegionCacheContext entityInfo)
    {
        await InvalidatePatternAsync(entityInfo);
    }

    public async Task InvalidateDeleteAsync(
        RegionCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);


        var cityInfos =
            await GetCityInfos(entityInfo.Id);

        foreach (var cityInfo in cityInfos)
        {
            await _cityInvalid
                .InvalidateDeleteAsync(cityInfo);
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
        RegionCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);


        var cityInfos =
           await GetCityInfos(entityInfo.Id);

        foreach (var cityInfo in cityInfos)
        {
            await _cityInvalid
                .InvalidateUpdateAsync(cityInfo);
        }
    }

    private async Task InvalidateAsync(
        RegionCacheContext entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys
            .EntityLanguagePattern<Region>(entityInfo.Id));

        await InvalidatePatternAsync(entityInfo);
    }

    private async Task InvalidatePatternAsync(
        RegionCacheContext entityInfo)
    {
        await _cache.RemoveByPatternAsync(CacheKeys.
           EntityLanguagePagedPattern<Region>(
                entityInfo.Status));

        await _cache.RemoveByPatternAsync(CacheKeys.
             LanguageSearchPattern<Region>(
                entityInfo.Status));
    }

    private async Task<List<CityCacheContext>> GetCityInfos(
        long entityId)
    {
        return await _cityRepository
            .GetCacheInfoByRegionAsync(entityId);
    }

    private async Task<List<TranslationCacheContext>> GetTranslationInfos(
        long entityId)
    {
        return await _transRepository
            .GetCacheInfosByTranslatableAsync(entityId);
    }
}
