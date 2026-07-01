using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.DTOs.Location.Regions;
using ShagOxServer.Application.Interfaces.Location.Regions.Query;
using ShagOxServer.Application.Services.Location.Regions.Mapping;
using ShagOxServer.Infrastructure.Interfaces.Location.Regions;

namespace ShagOxServer.Application.Services.Location.Regions.Query;
public class RegionQueryService : IRegionQueryService
{
    private readonly IRegionQueryRepository _repository;

    public RegionQueryService(
        IRegionQueryRepository regionRepository)
    {
        _repository = regionRepository;
    }

    public async Task<Result<RegionDto>> GetByIdAsync(int id)
    {
        var region = await _repository.GetByIdAsync(id);

        return region.ToResult(RegionMapper.ToDto);
    }

    public async Task<Result<RegionDto>> GetByNameAsync(string name)
    {
        var region = await _repository.GetByNameAsync(name);

        return region.ToResult(RegionMapper.ToDto);
    }

    public async Task<Result<List<RegionDto>>> SearchByName(
      string name,
      int page,
      int pageSize)
    {
        var regions = await _repository.SearchByName(name, page, pageSize);

        return regions.ToResultList(RegionMapper.ToDto);
    }
}
