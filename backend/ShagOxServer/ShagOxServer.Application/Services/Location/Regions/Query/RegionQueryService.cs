using ShagOxServer.Application.DTOs.Location.Regions;
using ShagOxServer.Application.Interfaces.Repositories.Location.Regions;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Query;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Location.Regions.Mapping;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Regions;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Location.Regions.Query;
public class RegionQueryService 
    : BaseQueryService<
        RegionDto,
        Region,
        RegionSearchFilter
        >,
    IRegionQueryService
{
    private readonly IRegionQueryRepository _regionQueryRepository;

    
    public RegionQueryService(
        IRegionQueryRepository regionQueryRepository
    )
        : base(regionQueryRepository)
    {
        _regionQueryRepository = regionQueryRepository;
    }


    protected override RegionDto ApplyMapper(
        Region entity)
    {
        return RegionMapper.ToDto(entity);
    }

    public async Task<Result<RegionDto>> GetByNameAsync(
        string code)
    {
        var region = await _regionQueryRepository
            .GetByCodeAsync(code);

        return region.ToResult(RegionMapper.ToDto);
    }
}