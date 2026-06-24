using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Advertisements.Create;
using ShagOxServer.Application.Interfaces.Advertisements.Create;
using ShagOxServer.Application.Interfaces.Advertisements.Query;
using ShagOxServer.Application.Common.Results.Extensions;

namespace ShagOxServer.Api.Controllers;

[ApiController]
[Route("api/advertisements")]
public class AdvertisementController : ControllerBase
{
    private readonly IAdvertisementCreateService _createService;
    private readonly IAdvertisementQueryService _queryService;

    public AdvertisementController(
        IAdvertisementCreateService createService,
        IAdvertisementQueryService queryService)
    {
        _createService = createService;
        _queryService = queryService;
    }

    [HttpPost]
    public async Task<IActionResult> Add(AdvertisementAddRequest request)
    {
        var result = await _createService.AddAdvertisementAsync(request);
        return result.ToActionResult();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int page = 1, int pageSize = 20)
    {
        var result = await _queryService.GetAllAsync(page, pageSize);
        return result.ToActionResult();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _queryService.GetByIdAsync(id);
        return result.ToActionResult();
    }

    [HttpGet("category/{categoryId}")]
    public async Task<IActionResult> GetByCategory(int categoryId)
    {
        var result = await _queryService.GetByCategoryAsync(categoryId);
        return result.ToActionResult();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(string query)
    {
        var result = await _queryService.SearchAsync(query);
        return result.ToActionResult();
    }
}