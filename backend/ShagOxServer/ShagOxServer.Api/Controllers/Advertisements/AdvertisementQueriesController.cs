using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Advertisements.Query;
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
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var result = await _queryService.GetAllAsync(page, pageSize);
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
        [FromQuery] int page,
        [FromQuery] int pageSize)
    {
        var result = await _queryService.GetByCategoryAsync(categoryId, page, pageSize);
        return result.ToActionResult();
    }

    [HttpGet("search/title")]
    public async Task<IActionResult> SearchByTitle(
        [FromQuery] string title,
        [FromQuery] int page,
        [FromQuery] int pageSize)
    {
        var result = await _queryService.SearchByTitle(title, page, pageSize);
        return result.ToActionResult();
    }

    [HttpGet("search/description")]
    public async Task<IActionResult> SearchByDescription(
       [FromQuery] string query,
        [FromQuery] int page,
        [FromQuery] int pageSize)
    {
        var result = await _queryService.SearchByDescription(query, page, pageSize);
        return result.ToActionResult();
    }
}