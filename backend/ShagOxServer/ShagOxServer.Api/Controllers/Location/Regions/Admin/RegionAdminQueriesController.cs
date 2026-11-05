using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Location.Regions.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Query;
using ShagOxServer.Domain.Filters.Location.Regions;

namespace ShagOxServer.Api.Controllers.Location.Regions.Admin;

[ApiController]
[Route("api/admin/regions")]
[Authorize(Roles = "Admin")]
public class RegionAdminQueriesController
    : BaseAdminQueryController<RegionDto,
        RegionSearchFilter>
{
    public RegionAdminQueriesController(
        IRegionQueryService queryService,
        IUserContext userContext
    )
        : base(queryService, userContext)
    {
    }
}
