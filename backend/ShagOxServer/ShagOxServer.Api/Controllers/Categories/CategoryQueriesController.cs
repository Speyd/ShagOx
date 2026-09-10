using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Query;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Category;

[ApiController]
[Route("api/categories")]
public class CategoryQueriesController 
    : ApiController
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
        var result = await _queryService
            .GetByIdAsync(id);

        return result.ToActionResult();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(
       [FromQuery] CategorySearchFilter filter,
       [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .Search(filter, pagination);

        return result.ToActionResult();
    }
}