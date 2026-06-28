using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Users.Update;
using ShagOxServer.Application.Interfaces.Users.Delete;
using ShagOxServer.Application.Interfaces.Users.Query;
using ShagOxServer.Application.Interfaces.Users.Update;
using ShagOxServer.Application.Common.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Users.Admin;

[ApiController]
[Route("api/admin/users")]
[Authorize(Roles = "Admin")]
public class UserAdminCommandsController : ControllerBase
{
    private readonly IUserDeleteService _deleteService;
    private readonly IUserUpdateService _updateService;

    public UserAdminCommandsController(
        IUserDeleteService deleteService,
        IUserUpdateService updateService)
    {
        _deleteService = deleteService;
        _updateService = updateService;
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _deleteService.DeleteUserAsync(id);
        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UserUpdateRequest request)
    {
        var result = await _updateService.UpdateUserAsync(id, request);
        return result.ToActionResult();
    }
}
