using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Location.Cities.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities.Query;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Query;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Query;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Caches.Keys.Dictionaries;
using ShagOxServer.Application.Services.Caches.Keys.Location;
using ShagOxServer.Application.Services.Location.Cities.Mapping;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Cities;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Location.Cities.Query;
public class CityQueryService 
    : BaseTranslatableQueryService<
        CityDto,
        City,
        CitySearchFilter
        >,
    ICityQueryService
{
    private readonly ICityQueryRepository _repositoryQueryCity;

    private readonly ICityTranslationQueryRepository _translationCityRepository;
    private readonly IRegionQueryService _regionService;


    public CityQueryService(
        ICityQueryRepository repositoryQueryCity,
        ICityTranslationQueryRepository translationCityRepository,
        IRegionQueryService regionService,
        ILanguageProvider language,
        ICacheService cacheService,
        IOptions<CacheSettings> settings
    )
        : base(repositoryQueryCity, language, cacheService, settings)
    {
        _repositoryQueryCity = repositoryQueryCity;
        _translationCityRepository = translationCityRepository;
        _regionService = regionService;
    }


    public override async Task<CityDto> ApplyMapperAsync(
        City entity)
    {
        var translationCity = await _translationCityRepository
           .GetByIdentificatorAsync(entity.Code, _language.Language);

        var region = await _regionService
           .ApplyMapperAsync(entity.Region);

        return CityMapper.ToDto(entity,
            region,
            translationCity?.Name);
    }

    public async Task<Result<PagedResult<CityDto>>> GetByRegionAsync(
        long regionId,
        PaginationParams pagination)
    {
        var cacheKey = CityCache.ByRegion(
           regionId,
           _language.Language,
           pagination.Page,
           pagination.PageSize);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var cities = await _repositoryQueryCity
                    .GetByRegionAsync(regionId, pagination);

                return await cities.ToResultPagedAsync(
                    ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }
}