using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.DTOs.Users.Update;
using ShagOxServer.Application.Interfaces.Users.Delete;
using ShagOxServer.Application.Interfaces.Users.Query;
using ShagOxServer.Application.Interfaces.Users.Update;

namespace ShagOxServer.Api.Controllers.Users;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserQueryService _queryService;
    private readonly IUserUpdateService _updateService;
    private readonly IUserDeleteService _deleteService;

    public UserController(
        IUserQueryService queryService,
        IUserUpdateService updateService,
        IUserDeleteService deleteService)
    {
        _queryService = queryService;
        _updateService = updateService;
        _deleteService = deleteService;
    }

    [HttpGet("{id}")]
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
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(UserUpdateRequest request)
    {
        var result = await _updateService.UpdateUserAsync(request);
        return result.ToActionResult();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _deleteService.DeleteUserAsync(id);
        return result.ToActionResult();
    }
}