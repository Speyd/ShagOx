using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.Interfaces.Specification.Currencies.Query;

namespace ShagOxServer.Api.Controllers.Currencies;

[ApiController]
[Route("api/admin/currencies")]
[Authorize(Roles = "Admin")]
public class CurrencyQueriesController : ControllerBase
{
    private readonly ICurrencyQueryService _queryService;

    public CurrencyQueriesController(
        ICurrencyQueryService queryService)
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

    [HttpGet("by-code")]
    public async Task<IActionResult> GetById(
        [FromQuery] string code)
    {
        var result = await _queryService.GetByCodeAsync(code);
        return result.ToActionResult();
    }

    [HttpGet("by-symbol")]
    public async Task<IActionResult> GetBySymbol(
        [FromQuery] string symbol)
    {
        var result = await _queryService.GetBySymbolAsync(symbol);
        return result.ToActionResult();
    }

    [HttpGet("search/code")]
    public async Task<IActionResult> SearchByCode(
       [FromQuery] string code,
       [FromQuery] int page = 1,
       [FromQuery] int pageSize = 20)
    {
        var result = await _queryService.SearchByCode(code, page, pageSize);
        return result.ToActionResult();
    }

    [HttpGet("search/name")]
    public async Task<IActionResult> SearchByName(
       [FromQuery] string name,
       [FromQuery] int page = 1,
       [FromQuery] int pageSize = 20)
    {
        var result = await _queryService.SearchByName(name, page, pageSize);
        return result.ToActionResult();
    }
}