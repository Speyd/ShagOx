using ShagOxServer.Application.DTOs.Location.Regions;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Filters.Location.Regions;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Location.Regions.Query;
public interface IRegionQueryService
    : IQueryService<RegionDto, RegionSearchFilter>
{
    Task<Result<RegionDto>> GetByNameAsync(
        string name);
}