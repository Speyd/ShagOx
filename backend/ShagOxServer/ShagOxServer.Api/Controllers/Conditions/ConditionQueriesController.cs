using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Specification.Conditions.Query;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;
using ShagOxServer.SharedKernel.Paginations;

namespace ShagOxServer.Api.Controllers.Conditions;

[ApiController]
[Route("api/admin/conditions")]
[Authorize(Roles = "Admin")]
public class ConditionQueriesController : ControllerBase
{
    private readonly IConditionQueryService _queryService;

    public ConditionQueriesController(
        IConditionQueryService queryService)
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
       [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService.SearchByName(name, pagination);
        return result.ToActionResult();
    }
}
