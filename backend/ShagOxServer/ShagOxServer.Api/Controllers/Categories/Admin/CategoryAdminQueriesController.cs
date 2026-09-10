using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Categories.Admin;
[ApiController]
[Route("api/admin/categories")]
[Authorize(Roles = "Admin")]
public class CategoryAdminQueriesController 
    : ApiController
{
    private readonly ICategoryQueryService _queryService;


    public CategoryAdminQueriesController(
        ICategoryQueryService queryService)
    {
        _queryService = queryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .GetPagedAsync(pagination);

        return result.ToActionResult();
    }
}