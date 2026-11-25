using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Common.Options.Systems.Cookies;
using ShagOxServer.Application.Interfaces.Services.Auth.Core;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Management;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using CookieOptions = ShagOxServer.Application.Common.Options.Systems.Cookies.CookieOptions;
namespace ShagOxServer.Api.Controllers.Auth.Core;

[ApiController]
[Route("api/admin/auth")]
[Authorize(Roles = "Admin")]
public class AuthAdminController
    : ApiCookieController
{
    private readonly ILogoutService _logoutService;
    private readonly IUserModerationService _moderationService;


    public AuthAdminController(
        ILogoutService logoutService,
        IUserModerationService moderationService,
        IOptions<CookieOptions> cookieOptions,
        IOptions<JwtOptions> jwtOptions,
        IOptions<RefreshTokenOptions> refreshOptions,
        ILogger<ApiCookieController> logger,
        IUserContext userContext
    )
        : base(cookieOptions,
            jwtOptions,
            refreshOptions,
            logger,
            userContext)
    {
        _logoutService = logoutService;
        _moderationService = moderationService;
    }


    [HttpPost("{userId:long}/logout-all")]
    [Authorize]
    public async Task<IActionResult> LogoutAll(
        [FromRoute] long userId,
        CancellationToken cancellationToken)
    {
        await _logoutService.LogoutAllAsync(
            userId,
            cancellationToken);

        DeleteTokens(Response);

        return Ok(new { message = "Logged out from all devices successfully" });
    }

    [HttpPost("{userId:long}/logout-other")]
    [Authorize]
    public async Task<IActionResult> LogoutOther(
        [FromRoute] long userId,
        CancellationToken cancellationToken)
    {
        await _logoutService.LogoutOtherSessions(
            userId,
            cancellationToken);

        DeleteTokens(Response);

        return Ok(new { message = "Logged out from other devices successfully" });
    }

    [HttpPost("user/{userId:long}/ban")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> BanUser(
        [FromRoute] long userId,
        CancellationToken cancellationToken,
        [FromQuery] DateTime? blockedUntil = null)
    {
        await _moderationService.BanUser(
            userId,
            blockedUntil,
            cancellationToken);

        return Ok(new
        {
            message = "User has been banned successfully"
        });
    }

    [HttpPost("user/{userId:long}/unban")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UnbanUser(
        [FromRoute] long userId,
        CancellationToken cancellationToken)
    {
        await _moderationService.UnbanUser(
            userId,
            cancellationToken);

        return Ok(new
        {
            message = "User has been unbanned successfully"
        });
    }

    [HttpPost("device/{deviceId:long}/ban")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> BanDevice(
        [FromRoute] long deviceId,
        CancellationToken cancellationToken,
        [FromQuery] DateTime? blockedUntil = null)
    {
        await _moderationService.BanDevice(
            deviceId,
            blockedUntil,
            cancellationToken);

        return Ok(new
        {
            message = "Device has been banned successfully"
        });
    }

    [HttpPost("device/{deviceId:long}/unban")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UnbanDevice(
        [FromRoute] long deviceId,
        CancellationToken cancellationToken)
    {
        await _moderationService.UnbanDevice(
            deviceId,
            cancellationToken);

        return Ok(new
        {
            message = "Device has been unbanned successfully"
        });
    }
}
