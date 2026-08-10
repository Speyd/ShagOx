using ShagOxServer.Application.DTOs.Location.Regions;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Query;
using ShagOxServer.Application.Services.Location.Regions.Mapping;
using ShagOxServer.Domain.Filters.Location.Regions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Location.Regions.Query;
public class RegionQueryService 
    : IRegionQueryService
{
    private readonly IRegionQueryRepository _regionQueryRepository;

    
    public RegionQueryService(
        IRegionQueryRepository regionQueryRepository)
    {
        _regionQueryRepository = regionQueryRepository;
    }


    public async Task<Result<RegionDto>> GetByIdAsync(
        int id)
    {
        var region = await _regionQueryRepository
            .GetByIdAsync(id);

        return region.ToResult(RegionMapper.ToDto);
    }

    public async Task<Result<PagedResult<RegionDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var regions = await _regionQueryRepository
            .GetPagedAsync(pagination);

        return regions.ToResultPaged(RegionMapper.ToDto);
    }

    public async Task<Result<RegionDto>> GetByNameAsync(
        string name)
    {
        var region = await _regionQueryRepository
            .GetByNameAsync(name);

        return region.ToResult(RegionMapper.ToDto);
    }

    public async Task<Result<PagedResult<RegionDto>>> Search(
      RegionSearchFilter filter,
	  PaginationParams pagination)
    {
        var regions = await _regionQueryRepository
            .Search(filter, pagination);

        return regions.ToResultPaged(RegionMapper.ToDto);
    }
}