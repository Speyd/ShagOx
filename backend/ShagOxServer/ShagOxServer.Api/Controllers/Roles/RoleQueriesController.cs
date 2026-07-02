using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Roles.Query;
using ShagOxServer.Application.Interfaces.UserRoles;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Role;

[ApiController]
[Route("api/admin/roles")]
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
    public async Task<IActionResult> GetById(
        [FromRoute] int id)
    {
        var result = await _queryService.GetByIdAsync(id);
        return result.ToActionResult();
    }

    [HttpGet("{roleId:int}/users")]
    public async Task<IActionResult> GetByRole(
        [FromRoute] int roleId,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryUserRoleService.GetUsersByRoleIdAsync(roleId, pagination);
        return result.ToActionResult();
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchByName(
        [FromQuery] string name,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService.SearchByName(name, pagination);
        return result.ToActionResult();
    }
}
