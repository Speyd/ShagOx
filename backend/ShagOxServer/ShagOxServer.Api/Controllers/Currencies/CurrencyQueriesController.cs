using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Specification.Currencies.Query;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;
using ShagOxServer.SharedKernel.Paginations;

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
       [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService.SearchByCode(code, pagination);
        return result.ToActionResult();
    }

    [HttpGet("search/name")]
    public async Task<IActionResult> SearchByName(
       [FromQuery] string name,
       [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService.SearchByName(name, pagination);
        return result.ToActionResult();
    }
}