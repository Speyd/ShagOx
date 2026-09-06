using ShagOxServer.Application.DTOs.Location.Cities.Translations;
using ShagOxServer.Application.Interfaces.Repositories.Location.Cities.Translations;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Query;
using ShagOxServer.Application.Services.Location.Cities.Translations.Mapping;
using ShagOxServer.Domain.Filters.Location.Cities.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Location.Cities.Translations.Query;
public class CityTranslationQueryService
    : ICityTranslationQueryService
{
    private readonly ICityTranslationQueryRepository _cityRepository;


    public CityTranslationQueryService(
        ICityTranslationQueryRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }


    public async Task<Result<CityTranslationDto>> GetByIdAsync(
        int id)
    {
        var city = await _cityRepository
            .GetByIdAsync(id);

        return city.ToResult(CityTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<CityTranslationDto>>> GetPagedAsync(
        PaginationParams pagination,
        string language)
    {
        var cities = await _cityRepository
            .GetPagedAsync(pagination, language);

        return cities.ToResultPaged(CityTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<CityTranslationDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var cities = await _cityRepository
            .GetPagedAsync(pagination);

        return cities.ToResultPaged(CityTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<CityTranslationDto>>> Search(
        CityTranslationSearchFilter filter,
        PaginationParams pagination)
    {
        var cities = await _cityRepository
            .Search(filter, pagination);

        return cities.ToResultPaged(CityTranslationMapper.ToDto);
    }
}