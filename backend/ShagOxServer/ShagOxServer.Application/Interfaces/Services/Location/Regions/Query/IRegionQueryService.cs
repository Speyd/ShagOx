using ShagOxServer.Application.DTOs.Location.Regions;
using ShagOxServer.Application.Interfaces.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Location;
using ShagOxServer.Domain.Filters.Location.Regions;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Location.Regions.Query;
public interface IRegionQueryService
    : ITranslatableQueryService<RegionDto, 
        Region, 
        RegionSearchFilter>
{
}