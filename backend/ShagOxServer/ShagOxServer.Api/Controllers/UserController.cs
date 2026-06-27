using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.DTOs.Users.Update;
using ShagOxServer.Application.Interfaces.Users.Delete;
using ShagOxServer.Application.Interfaces.Users.Query;
using ShagOxServer.Application.Interfaces.Users.Update;

namespace ShagOxServer.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserQueryService _queryService;
    private readonly IUserAdminQueryService _queryAdminService;
    private readonly IUserUpdateService _updateService;
    private readonly IUserDeleteService _deleteService;


    public UserController(
        IUserQueryService queryService,
        IUserAdminQueryService queryAdminService,
        IUserUpdateService updateService,
        IUserDeleteService deleteService)
    {
        _queryService = queryService;
        _queryAdminService = queryAdminService;
        _updateService = updateService;
        _deleteService = deleteService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _queryService.GetByIdAsync(id);
        return result.ToActionResult();
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMyProfile()
    {
        var result = await _queryService.GetMyProfileAsync();
        return result.ToActionResult();
    }

    [HttpGet("by-contact")]
    public async Task<IActionResult> GetByContact(
        [FromQuery] string? email,
        [FromQuery] string? phone)
    {
        var result = await _queryAdminService.GetByContactAsync(email, phone);
        return result.ToActionResult();
    }

    [HttpGet("by-city/{cityId}")]
    public async Task<IActionResult> GetByCity(int cityId)
    {
        var result = await _queryAdminService.GetByCityAsync(cityId);
        return result.ToActionResult();
    }

    [HttpGet("registered-after")]
    public async Task<IActionResult> GetRegisteredAfter([FromQuery] DateTime date)
    {
        var result = await _queryAdminService.GetUsersRegisteredAfterAsync(date);
        return result.ToActionResult();
    }

    [HttpGet("active-after")]
    public async Task<IActionResult> GetActiveAfter([FromQuery] DateTime date)
    {
        var result = await _queryAdminService.GetUsersActiveAfterAsync(date);
        return result.ToActionResult();
    }

    [HttpGet("me/role")]
    public async Task<IActionResult> GetMyRole()
    {
        var result = await _queryService.GetMyProfileAsync();
        if (!result.IsSuccess || result.Value is null)
            return BadRequest(result.Error);

        var roles = result.Value.Roles;

        return Ok(roles);
    }

    [HttpGet("{id}/roles")]
    public async Task<IActionResult> GetUserRoles(int id)
    {
        var result = await _queryService.GetByIdAsync(id);
        if (!result.IsSuccess || result.Value is null)
            return BadRequest(result.Error);

        var roles = result.Value.Roles;
        return result.ToActionResult();
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(UserUpdateRequest request)
    {
        var result = await _updateService.UpdateUserAsync(request);
        return result.ToActionResult();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _deleteService.DeleteUserAsync(id);
        return result.ToActionResult();
    }
}