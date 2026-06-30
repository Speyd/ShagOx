using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Specification.Conditions.Query;
using ShagOxServer.Application.Common.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Conditions;
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
