using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Advertisements.Query;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Advertisements;

[ApiController]
[Route("api/advertisements")]
public class AdvertisementQueriesController : ControllerBase
{
    private readonly IAdvertisementQueryService _queryService;

    public AdvertisementQueriesController(
        IAdvertisementQueryService queryService)
    {
        _queryService = queryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService.GetAllAsync(pagination);
        return result.ToActionResult();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        [FromRoute] int id)
    {
        var result = await _queryService.GetByIdAsync(id);
        return result.ToActionResult();
    }

    [HttpGet("category/{categoryId:int}")]
    public async Task<IActionResult> GetByCategory(
        [FromRoute] int categoryId,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService.GetByCategoryAsync(categoryId, pagination);
        return result.ToActionResult();
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchByTitle(
        [FromQuery] AdvertisementSearchFilter filter,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService.Search(filter, pagination);
        return result.ToActionResult();
    }
}