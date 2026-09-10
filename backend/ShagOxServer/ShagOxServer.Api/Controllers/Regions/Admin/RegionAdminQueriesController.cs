using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;


namespace ShagOxServer.Api.Controllers.Regions.Admin;

[ApiController]
[Route("api/admin/regions")]
[Authorize(Roles = "Admin")]
public class RegionAdminQueriesController 
    : ApiController
{
    private readonly IRegionQueryService _queryService;


    public RegionAdminQueriesController(
        IRegionQueryService queryService)
    {
        _queryService = queryService;
    }


    
    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .GetPagedAsync(pagination);

        return result.ToActionResult();
    }
}