using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Common.Options.Systems.Cookies;
using ShagOxServer.Application.Interfaces.Services.Auth.Core;
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


    public AuthAdminController(
        ILogoutService logoutService,
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
}
