using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Auth.Users.Update;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Delete;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Users.Admin;

[ApiController]
[Route("api/admin/users")]

public class UserAdminCommandsController : ApiController
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
    public async Task<IActionResult> Delete(
        [FromRoute] int id)
    {
        var result = await _deleteService
            .DeleteAsync(id);

        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromForm] UserUpdateRequest request)
    {
        var result = await _updateService
            .UpdateAsync(id, request);

        return result.ToActionResult();
    }
}