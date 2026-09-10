using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Cities.Admin;

[ApiController]
[Route("api/admin/cities")]
[Authorize(Roles = "Admin")]
public class CityAdminQueriesController 
    : ApiController
{
    private readonly ICityQueryService _queryService;


    public CityAdminQueriesController(
        ICityQueryService queryService)
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