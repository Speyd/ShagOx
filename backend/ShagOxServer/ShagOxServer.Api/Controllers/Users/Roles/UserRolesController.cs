using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.Interfaces.UserRoles;
using ShagOxServer.Application.Interfaces.Users.Query;

namespace ShagOxServer.Api.Controllers.Users.Roles;

[ApiController]
[Route("api/users")]
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
    [HttpGet("me/role")]
    public async Task<IActionResult> GetMyRole()
    {
        var result = await _queryService.GetMyProfileAsync();

        if (!result.IsSuccess || result.Value is null)
            return BadRequest(result.Error);

        var roles = result.Value.Roles;
        return Ok(roles);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id:int}/roles")]
    public async Task<IActionResult> GetUserRoles(
        [FromRoute] int id)
    {
        var result = await _queryService.GetByIdAsync(id);

        if (!result.IsSuccess || result.Value is null)
            return BadRequest(result.Error);

        var roles = result.Value.Roles;
        return Ok(roles);
    }
}