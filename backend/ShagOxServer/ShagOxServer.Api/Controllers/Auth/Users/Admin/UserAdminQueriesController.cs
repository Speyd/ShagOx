using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Filters.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Auth.Users.Admin;

[ApiController]
[Route("api/admin/users")]
[Authorize(Roles = "Admin")]
public class UserAdminQueriesController
    : ApiController
{
    private readonly IUserAdminQueryService _queryService;


    public UserAdminQueriesController(
        IUserAdminQueryService queryService,
        IUserContext userContext
    )
        : base(userContext)
    {
        _queryService = queryService;
    }


    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery] PaginationParams pagination,
        [FromQuery] EntityStatus status,
        CancellationToken cancellationToken)
    {
        var result = await _queryService
            .GetPagedAsync(pagination, 
                cancellationToken, status);

        return result.ToActionResult();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] UserAdminSearchFilter filter,
        [FromQuery] PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        var result = await _queryService
            .Search(filter, pagination,
                cancellationToken);

        return result.ToActionResult();
    }
}
