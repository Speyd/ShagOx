using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Specification.Currencies.Query;
using ShagOxServer.Application.Common.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Currencies;

public class CurrencyQueriesController : ControllerBase
{
    private readonly ICurrencyQueryService _queryService;

    public CurrencyQueriesController(
        ICurrencyQueryService queryService)
    {
        _queryService = queryService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _queryService.GetByIdAsync(id);
        return result.ToActionResult();
    }

    [HttpGet("by-code/{code}")]
    public async Task<IActionResult> GetById(string code)
    {
        var result = await _queryService.GetByCodeAsync(code);
        return result.ToActionResult();
    }

    [HttpGet("by-symbol/{symbol}")]
    public async Task<IActionResult> GetBySymbol(string symbol)
    {
        var result = await _queryService.GetBySymbolAsync(symbol);
        return result.ToActionResult();
    }
}
