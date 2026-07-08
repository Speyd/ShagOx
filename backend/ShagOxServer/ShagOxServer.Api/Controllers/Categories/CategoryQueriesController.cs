using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Dictionaries.Categories.Query;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Category;

[ApiController]
[Route("api/admin/categories")]
[Authorize(Roles = "Admin")]
public class CategoryQueriesController : ApiController
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
    public async Task<IActionResult> Search(
       [FromQuery] CategorySearchFilter filter,
       [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService.Search(filter, pagination);
        return result.ToActionResult();
    }
}
