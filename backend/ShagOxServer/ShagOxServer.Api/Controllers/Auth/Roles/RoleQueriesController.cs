using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Auth.Roles.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Roles.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.UserRoles.Query;
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
        IUserRoleQueryService queryUserRoleService
    )
        : base(queryService)
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
