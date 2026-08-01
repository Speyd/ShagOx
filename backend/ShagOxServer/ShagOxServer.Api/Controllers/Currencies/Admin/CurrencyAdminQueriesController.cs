using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Currencies.Admin;

[ApiController]
[Route("api/admin/currencies")]
[Authorize(Roles = "Admin")]
public class CurrencyAdminQueriesController : ApiController
{
    private readonly ICurrencyQueryService _queryService;


    public CurrencyAdminQueriesController(
        ICurrencyQueryService queryService)
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