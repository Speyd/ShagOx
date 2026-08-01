using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;
using ShagOxServer.Application.DTOs.Auth.Users.Update;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Update;

namespace ShagOxServer.Api.Controllers.Users;

[ApiController]
[Route("api/users")]
public class UserCommandsController : ApiController
{
    private readonly IUserUpdateService _updateService;


    public UserCommandsController(
        IUserUpdateService updateService)
    {
        _updateService = updateService;
    }


    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] UserUpdateRequest request)
    {
        if (UserId != id)
            return Forbid();

        var result = await _updateService
            .UpdateAsync(id, request);

        return result.ToActionResult();
    }
}