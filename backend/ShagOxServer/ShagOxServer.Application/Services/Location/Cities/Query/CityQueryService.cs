using ShagOxServer.Application.DTOs.Location.Cities;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities.Translations;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Query;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Query;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Location.Cities.Mapping;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Cities;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Location.Cities.Query;
public class CityQueryService 
    : BaseQueryService<
        CityDto,
        City,
        CitySearchFilter
        >,
    ICityQueryService
{
    private readonly ICityQueryRepository _repositoryQueryCity;

    private readonly ICityTranslationQueryRepository _translationCityRepository;
    private readonly IRegionQueryService _regionService;

    private readonly ILanguageProvider _language;


    public CityQueryService(
        ICityQueryRepository repositoryQueryCity,
        ICityTranslationQueryRepository translationCityRepository,
        IRegionQueryService regionService,
        ILanguageProvider language
    )
        : base(repositoryQueryCity)
    {
        _repositoryQueryCity = repositoryQueryCity;
        _translationCityRepository = translationCityRepository;
        _regionService = regionService;
        _language = language;
    }


    public override async Task<CityDto> ApplyMapperAsync(
        City entity)
    {
        var translationCity = await _translationCityRepository
           .GetByIdentificatorAsync(entity.Code, _language.Language);

        var translationRegion = await _regionService
           .ApplyMapperAsync(entity.Region);

        return CityMapper.ToDto(entity,
            translationRegion,
            translationCity?.Name);
    }

    public async Task<Result<CityDto>> GetByCodeAsync(
        string code)
    {
        var city = await _repositoryQueryCity
            .GetByCodeAsync(code);

        return await city.ToResultAsync(ApplyMapperAsync);
    }

    public async Task<Result<PagedResult<CityDto>>> GetByRegionAsync(
        long regionId,
        PaginationParams pagination)
    {
        var cities = await _repositoryQueryCity
            .GetByRegionAsync(regionId, pagination);

        return await cities.ToResultPagedAsync(ApplyMapperAsync);
    }
}