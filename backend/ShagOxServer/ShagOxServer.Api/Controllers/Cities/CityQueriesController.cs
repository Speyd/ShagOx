using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Query;
using ShagOxServer.Domain.Filters.Location.Cities;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Cities;

[ApiController]
[Route("api/cities")]
public class CityQueriesController : ApiController
{
    private readonly ICityQueryService _queryService;


    public CityQueriesController(
        ICityQueryService queryService)
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
       [FromQuery] CitySearchFilter filter,
       [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .Search(filter, pagination);

        return result.ToActionResult();
    }
}