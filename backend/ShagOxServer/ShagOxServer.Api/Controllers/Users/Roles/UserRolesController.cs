using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.Interfaces.UserRoles;
using ShagOxServer.Application.Interfaces.Users.Query;

namespace ShagOxServer.Api.Controllers.Users.Roles;

[ApiController]
public class UserRolesController : ControllerBase
{
    private readonly IUserQueryService _queryService;
    private readonly IUserRoleService _queryUserRoleService;

    public UserRolesController(
        IUserQueryService queryService,
        IUserRoleService queryUserRoleService)
    {
        _queryService = queryService;
        _queryUserRoleService = queryUserRoleService;
    }

    [Authorize]
    [HttpGet("api/users/me/role")]
    public async Task<IActionResult> GetMyRole()
    {
        var result = await _queryService.GetMyProfileAsync();

        if (!result.IsSuccess || result.Value is null)
            return BadRequest(result.Error);

        var roles = result.Value.Roles;
        return Ok(roles);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("api/users/{id:int}/roles")]
    public async Task<IActionResult> GetUserRoles(int id)
    {
        var result = await _queryService.GetByIdAsync(id);

        if (!result.IsSuccess || result.Value is null)
            return BadRequest(result.Error);

        var roles = result.Value.Roles;
        return Ok(roles);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("api/admin/users/by-role/{roleId:int}")]
    public async Task<IActionResult> GetByRole(int roleId)
    {
        var result = await _queryUserRoleService.GetUsersByRoleIdAsync(roleId);
        return result.ToActionResult();
    }
}