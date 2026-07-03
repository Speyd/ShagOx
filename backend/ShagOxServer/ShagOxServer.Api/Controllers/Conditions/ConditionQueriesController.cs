using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Specification.Conditions.Query;
using ShagOxServer.Domain.Filters.Specification.Conditions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

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
    public async Task<IActionResult> Search(
       [FromQuery] ConditionSearchFilter filter,
       [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService.Search(filter, pagination);
        return result.ToActionResult();
    }
}
