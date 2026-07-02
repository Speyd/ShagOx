using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.SharedKernel.Results.Extensions;
using ShagOxServer.Application.Interfaces.Dictionaries.Categories.Query;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;

namespace ShagOxServer.Api.Controllers.Category;

[ApiController]
[Route("api/admin/categories")]
[Authorize(Roles = "Admin")]
public class CategoryQueriesController : ControllerBase
{
    private readonly ICategoryQueryService _queryService;

    public CategoryQueriesController(
        ICategoryQueryService queryService)
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

    [HttpGet("by-product-type")]
    public async Task<IActionResult> GetByProductType(
        [FromQuery] ProductType type)
    {
        var result = await _queryService.GetByProductTypeAsync(type);
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
