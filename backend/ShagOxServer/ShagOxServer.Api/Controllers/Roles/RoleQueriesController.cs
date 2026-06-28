using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.Interfaces.Roles.Query;
using ShagOxServer.Application.Interfaces.UserRoles;

namespace ShagOxServer.Api.Controllers.Role;

[ApiController]
[Route("api/admin/role")]
[Authorize(Roles = "Admin")]
public class RoleQueriesController : ControllerBase
{
    private readonly IRoleQueryService _queryService;
    private readonly IUserRoleService _queryUserRoleService;

    public RoleQueriesController(
        IRoleQueryService queryService,
        IUserRoleService queryUserRoleService)
    {
        _queryService = queryService;
        _queryUserRoleService = queryUserRoleService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _queryService.GetByIdAsync(id);
        return result.ToActionResult();
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{roleId:int}/users")]
    public async Task<IActionResult> GetByRole(
        int roleId,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var result = await _queryUserRoleService.GetUsersByRoleIdAsync(roleId, page, pageSize);
        return result.ToActionResult();
    }
}
