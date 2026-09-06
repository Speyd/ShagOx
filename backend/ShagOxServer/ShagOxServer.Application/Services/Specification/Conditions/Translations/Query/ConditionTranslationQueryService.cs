using ShagOxServer.Application.DTOs.Specification.Conditions.Translations;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions.Translations;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Query;
using ShagOxServer.Application.Services.Specification.Conditions.Translations.Mapping;
using ShagOxServer.Domain.Filters.Specification.Conditions.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Specification.Conditions.Translations.Query;
public class ConditionTranslationQueryService
    : IConditionTranslationQueryService
{
    private readonly IConditionTranslationQueryRepository _condtitionRepository;


    public ConditionTranslationQueryService(
        IConditionTranslationQueryRepository condtitionRepository)
    {
        _condtitionRepository = condtitionRepository;
    }


    public async Task<Result<ConditionTranslationDto>> GetByIdAsync(
        int id)
    {
        var city = await _condtitionRepository
            .GetByIdAsync(id);

        return city.ToResult(ConditionTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<ConditionTranslationDto>>> GetPagedAsync(
        PaginationParams pagination,
        string language)
    {
        var cities = await _condtitionRepository
            .GetPagedAsync(pagination, language);

        return cities.ToResultPaged(ConditionTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<ConditionTranslationDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var cities = await _condtitionRepository
            .GetPagedAsync(pagination);

        return cities.ToResultPaged(ConditionTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<ConditionTranslationDto>>> Search(
        ConditionTranslationSearchFilter filter,
        PaginationParams pagination)
    {
        var cities = await _condtitionRepository
            .Search(filter, pagination);

        return cities.ToResultPaged(ConditionTranslationMapper.ToDto);
    }
}