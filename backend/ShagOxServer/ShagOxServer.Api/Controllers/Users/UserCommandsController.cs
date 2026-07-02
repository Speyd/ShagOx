using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Users.Update;
using ShagOxServer.Application.Interfaces.Users.Query;
using ShagOxServer.Application.Interfaces.Users.Update;
using System.Security.Claims;
using ShagOxServer.SharedKernel.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Users;

[ApiController]
[Route("api/users")]
public class UserCommandsController : ControllerBase
{
    private readonly IUserQueryService _queryService;
    private readonly IUserUpdateService _updateService;

    public UserCommandsController(
        IUserQueryService queryService,
        IUserUpdateService updateService)
    {
        _queryService = queryService;
        _updateService = updateService;
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] UserUpdateRequest request)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        if (userId != id)
            return Forbid();

        var result = await _updateService.UpdateUserAsync(id, request);
        return result.ToActionResult();
    }
}
