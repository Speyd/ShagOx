using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Services.Auth.Roles.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Users.Roles;

[ApiController]
[Route("api/users")]
public class UserRolesController : ApiController
{
    private readonly IUserQueryService _userService;
    private readonly IRoleQueryService _roleService;


    public UserRolesController(
        IUserQueryService queryService,
        IRoleQueryService roleService)
    {
        _userService = queryService;
        _roleService = roleService;
    }


    [Authorize]
    [HttpGet("me/role")]
    public async Task<IActionResult> GetMyRole(
        [FromQuery] PaginationParams pagination)
    {
        var result = await _userService
            .GetMyRoleAsync(pagination);

        return result.ToActionResult();
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{userId:int}/roles")]
    public async Task<IActionResult> GetUserRoles(
        [FromRoute] int userId,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _roleService
            .GetByUserAsync(userId, pagination);

        return result.ToActionResult();
    }
}