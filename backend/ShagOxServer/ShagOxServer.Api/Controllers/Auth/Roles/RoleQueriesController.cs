using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
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
    : ApiController
{
    private readonly IRoleQueryService _queryService;
    private readonly IUserRoleQueryService _queryUserRoleService;


    public RoleQueriesController(
        IRoleQueryService queryService,
        IUserRoleQueryService queryUserRoleService)
    {
        _queryService = queryService;
        _queryUserRoleService = queryUserRoleService;
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(
        [FromRoute] long id)
    {
        var result = await _queryService
            .GetByIdAsync(id);

        return result.ToActionResult();
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .GetPagedAsync(pagination);

        return result.ToActionResult();
    }

    [HttpGet("{roleId:long}/users")]
    public async Task<IActionResult> GetByRole(
        [FromRoute] long roleId,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryUserRoleService
            .GetUsersByRoleAsync(roleId, pagination);

        return result.ToActionResult();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] RoleSearchFilter filter,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .SearchAsync(filter, pagination);

        return result.ToActionResult();
    }
}