using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Query;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.ProductTypes.Admin;

[ApiController]
[Route("api/admin/product-types")]
[Authorize(Roles = "Admin")]
public class ProductTypeAdminQueriesController 
    : ApiController
{
    private readonly IProductTypeQueryService _queryService;


    public ProductTypeAdminQueriesController(
        IProductTypeQueryService queryService)
    {
        _queryService = queryService;
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(
       [FromQuery] ProductTypeSearchFilter filter,
       [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .Search(filter, pagination);

        return result.ToActionResult();
    }
}