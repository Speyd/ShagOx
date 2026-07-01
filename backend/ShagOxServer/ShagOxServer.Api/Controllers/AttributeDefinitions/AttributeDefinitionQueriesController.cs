using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.Interfaces.Dictionaries.AttributeDefinitions.Query;

namespace ShagOxServer.Api.Controllers.AttributeDefinitions;

[ApiController]
[Route("api/admin/attributes")]
[Authorize(Roles = "Admin")]
public class AttributeDefinitionQueriesController : ControllerBase
{
    private readonly IAttributeDefinitionQueryService _queryService;

    public AttributeDefinitionQueriesController(
        IAttributeDefinitionQueryService queryService)
    {
        _queryService = queryService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _queryService.GetByIdAsync(id);
        return result.ToActionResult();
    }

    [HttpGet("by-category/{categoryId:int}")]
    public async Task<IActionResult> GetByName(int categoryId)
    {
        var result = await _queryService.GetByCategoryAsync(categoryId);
        return result.ToActionResult();
    }
    [HttpGet("search/name")]
    public async Task<IActionResult> SearchByKey(
        [FromQuery] string key,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _queryService.SearchByKey(key, page, pageSize);
        return result.ToActionResult();
    }
}
