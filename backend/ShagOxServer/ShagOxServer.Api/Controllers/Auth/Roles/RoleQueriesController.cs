using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Auth.Roles.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Roles.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.UserRoles.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Filters.Auth.Roles;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Auth.Roles;

[ApiController]
[Route("api/admin/roles")]
[Authorize(Roles = "Admin")]
public class RoleQueriesController 
    : BaseQueryController<RoleDto,
        RoleSearchFilter>
{
    private readonly IUserRoleQueryService _queryUserRoleService;


    public RoleQueriesController(
        IRoleQueryService queryService,
        IUserRoleQueryService queryUserRoleService,
        IUserContext userContext
    )
        : base(queryService, userContext)
    {
        _queryUserRoleService = queryUserRoleService;
    }


    [HttpGet("{roleId:long}/users")]
    public async Task<IActionResult> GetByRole(
        [FromRoute] long roleId,
        [FromQuery] PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        var result = await _queryUserRoleService
            .GetUsersByRoleAsync(roleId, pagination);

        return result.ToActionResult();
    }
}
