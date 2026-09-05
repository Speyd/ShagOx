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
        var status = await _cityRepository
            .GetByIdAsync(id);

        return status.ToResult(CityTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<CityTranslationDto>>> GetPagedAsync(
        PaginationParams pagination,
        string language)
    {
        var statuses = await _cityRepository
            .GetPagedAsync(pagination, language);

        return statuses.ToResultPaged(CityTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<CityTranslationDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var statuses = await _cityRepository
            .GetPagedAsync(pagination);

        return statuses.ToResultPaged(CityTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<CityTranslationDto>>> Search(
        CityTranslationSearchFilter filter,
        PaginationParams pagination)
    {
        var statuses = await _cityRepository
            .Search(filter, pagination);

        return statuses.ToResultPaged(CityTranslationMapper.ToDto);
    }
}