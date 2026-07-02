using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.Interfaces.Location.Regions.Query;

namespace ShagOxServer.Api.Controllers.Regions;

[ApiController]
[Route("api/admin/regions")]
[Authorize(Roles = "Admin")]
public class RegionQueriesController : ControllerBase
{
    private readonly IRegionQueryService _queryService;

    public RegionQueriesController(
        IRegionQueryService queryService)
    {
        _queryService = queryService;
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        [FromRoute] int id)
    {
        var result = await _queryService.GetByIdAsync(id);
        return result.ToActionResult();
    }

    [HttpGet("by-name")]
    public async Task<IActionResult> GetByName(
         [FromQuery] string name)
    {
        var result = await _queryService.GetByNameAsync(name);
        return result.ToActionResult();
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchByName(
       [FromQuery] string name,
       [FromQuery] int page = 1,
       [FromQuery] int pageSize = 20)
    {
        var result = await _queryService.SearchByName(name, page, pageSize);
        return result.ToActionResult();
    }
}
