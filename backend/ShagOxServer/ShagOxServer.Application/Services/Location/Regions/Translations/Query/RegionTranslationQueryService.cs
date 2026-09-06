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
        var region = await _regionRepository
            .GetByIdAsync(id);

        return region.ToResult(RegionTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<RegionTranslationDto>>> GetPagedAsync(
        PaginationParams pagination,
        string language)
    {
        var regions = await _regionRepository
            .GetPagedAsync(pagination, language);

        return regions.ToResultPaged(RegionTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<RegionTranslationDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var regions = await _regionRepository
            .GetPagedAsync(pagination);

        return regions.ToResultPaged(RegionTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<RegionTranslationDto>>> Search(
        RegionTranslationSearchFilter filter,
        PaginationParams pagination)
    {
        var regions = await _regionRepository
            .Search(filter, pagination);

        return regions.ToResultPaged(RegionTranslationMapper.ToDto);
    }
}