using ShagOxServer.Application.DTOs.Location.Regions.Translations;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions.Translations;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Translations.Query;
using ShagOxServer.Application.Services.Location.Regions.Translations.Mapping;
using ShagOxServer.Domain.Filters.Location.Regions.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Location.Regions.Translations.Query;
public class RegionTranslationQueryService
    : IRegionTranslationQueryService
{
    private readonly IRegionTranslationQueryRepository _regionRepository;


    public RegionTranslationQueryService(
        IRegionTranslationQueryRepository regionRepository)
    {
        _regionRepository = regionRepository;
    }


    public async Task<Result<RegionTranslationDto>> GetByIdAsync(
        int id)
    {
        var status = await _regionRepository
            .GetByIdAsync(id);

        return status.ToResult(RegionTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<RegionTranslationDto>>> GetPagedAsync(
        PaginationParams pagination,
        string language)
    {
        Console.WriteLine("\n\n\n");
        Console.WriteLine(language);

        var statuses = await _regionRepository
            .GetPagedAsync(pagination, language);

        return statuses.ToResultPaged(RegionTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<RegionTranslationDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var statuses = await _regionRepository
            .GetPagedAsync(pagination);

        return statuses.ToResultPaged(RegionTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<RegionTranslationDto>>> Search(
        RegionTranslationSearchFilter filter,
        PaginationParams pagination)
    {
        var statuses = await _regionRepository
            .Search(filter, pagination);

        return statuses.ToResultPaged(RegionTranslationMapper.ToDto);
    }
}