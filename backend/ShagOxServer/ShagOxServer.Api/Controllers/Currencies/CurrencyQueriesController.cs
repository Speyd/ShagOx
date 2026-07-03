using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Specification.Currencies.Query;
using ShagOxServer.Domain.Filters.Specification.Currencies;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

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

    [HttpGet("search")]
    public async Task<IActionResult> SearchByCode(
       [FromQuery] CurrencySearchFilter filter,
       [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService.Search(filter, pagination);
        return result.ToActionResult();
    }
}