using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Location.Regions.Query;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Query;
using ShagOxServer.Domain.Filters.Location.Regions;

namespace ShagOxServer.Api.Controllers.Location.Regions;

[ApiController]
[Route("api/regions")]
public class RegionQueriesController 
    : BaseQueryController<RegionDto,
        RegionSearchFilter>
{
    public RegionQueriesController(
        IRegionQueryService queryService
    )
        : base(queryService)
    {
    }
}
