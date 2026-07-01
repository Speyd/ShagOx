using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Location.Cities.Query;
using ShagOxServer.Application.Common.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Cities;

[ApiController]
[Route("api/admin/cities")]
[Authorize(Roles = "Admin")]
public class CityQueriesController : ControllerBase
{
    private readonly ICityQueryService _queryService;

    public CityQueriesController(
        ICityQueryService queryService)
    {
        _queryService = queryService;
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _queryService.GetByIdAsync(id);
        return result.ToActionResult();
    }

    [HttpGet("by-name/{name}")]
    public async Task<IActionResult> GetByName(
         [FromRoute] string name)
    {
        var result = await _queryService.GetByNameAsync(name);
        return result.ToActionResult();
    }

    [HttpGet("by-region/{regionId:int}")]
    public async Task<IActionResult> GetByRegion(
        [FromRoute] int regionId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20
        )
    {
        var result = await _queryService.GetByRegionAsync(
            regionId, 
            page, 
            pageSize);

        return result.ToActionResult();
    }

    //TODO: make PaginationQuery
    [HttpGet("search/name")]
    public async Task<IActionResult> SearchByName(
       [FromQuery] string name,
       [FromQuery] int page = 1,
       [FromQuery] int pageSize = 20)
    {
        var result = await _queryService.SearchByName(name, page, pageSize);
        return result.ToActionResult();
    }
}