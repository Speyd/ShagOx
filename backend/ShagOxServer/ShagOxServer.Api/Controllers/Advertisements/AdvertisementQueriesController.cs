using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Advertisements.Query;
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

    [HttpGet("search/title")]
    public async Task<IActionResult> SearchByTitle(
        [FromQuery] string title,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService.SearchByTitle(title, pagination);
        return result.ToActionResult();
    }

    [HttpGet("search/description")]
    public async Task<IActionResult> SearchByDescription(
       [FromQuery] string query,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService.SearchByDescription(query, pagination);
        return result.ToActionResult();
    }
}