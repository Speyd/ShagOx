using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Users.Query;
using ShagOxServer.Application.Common.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Users;

[ApiController]
[Route("api/admin/users")]
public class AdminUsersController : ControllerBase
{
    private readonly IUserAdminQueryService _queryAdminService;

    public AdminUsersController(IUserAdminQueryService queryAdminService)
    {
        _queryAdminService = queryAdminService;
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
    public async Task<IActionResult> GetRegisteredAfter(
        [FromQuery] DateTime date)
    {
        var result = await _queryAdminService.GetUsersRegisteredAfterAsync(date);
        return result.ToActionResult();
    }

    [HttpGet("active-after")]
    public async Task<IActionResult> GetActiveAfter(
        [FromQuery] DateTime date)
    {
        var result = await _queryAdminService.GetUsersActiveAfterAsync(date);
        return result.ToActionResult();
    }
}