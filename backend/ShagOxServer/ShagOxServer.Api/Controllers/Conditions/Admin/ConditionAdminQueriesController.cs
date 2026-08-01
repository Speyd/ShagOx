using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Conditions.Admin;

[ApiController]
[Route("api/admin/conditions")]
[Authorize(Roles = "Admin")]
public class ConditionAdminQueriesController : ApiController
{
    private readonly IConditionQueryService _queryService;


    public ConditionAdminQueriesController(
        IConditionQueryService queryService)
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