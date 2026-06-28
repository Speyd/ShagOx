using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.DTOs.Users.Update;
using ShagOxServer.Application.Interfaces.Users.Query;
using ShagOxServer.Application.Interfaces.Users.Update;
using System.Security.Claims;

namespace ShagOxServer.Api.Controllers.Users;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserQueryService _queryService;
    private readonly IUserUpdateService _updateService;

    public UserController(
        IUserQueryService queryService,
        IUserUpdateService updateService)
    {
        _queryService = queryService;
        _updateService = updateService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _queryService.GetByIdAsync(id);
        return result.ToActionResult();
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMyProfile()
    {
        var result = await _queryService.GetMyProfileAsync();
        return result.ToActionResult();
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(UserUpdateRequest request)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value); 
        if(userId != request.Id)
            return Forbid();

        var result = await _updateService.UpdateUserAsync(request);
        return result.ToActionResult();
    }
}