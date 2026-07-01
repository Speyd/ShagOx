using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.Interfaces.Specification.Conditions.Query;

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
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _queryService.GetByIdAsync(id);
        return result.ToActionResult();
    }

    [HttpGet("by-name/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        var result = await _queryService.GetByNameAsync(name);
        return result.ToActionResult();
    }
}
