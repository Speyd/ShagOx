using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Dictionaries.AttributeDefinitions.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

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
    public async Task<IActionResult> GetById(
        [FromRoute] int id)
    {
        var result = await _queryService.GetByIdAsync(id);
        return result.ToActionResult();
    }

    [HttpGet("by-category/{categoryId:int}")]
    public async Task<IActionResult> GetByName(
        [FromRoute] int categoryId)
    {
        var result = await _queryService.GetByCategoryAsync(categoryId);
        return result.ToActionResult();
    }
    [HttpGet("search")]
    public async Task<IActionResult> SearchByKey(
        [FromQuery] string key,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService.SearchByKey(key, pagination);
        return result.ToActionResult();
    }
}
