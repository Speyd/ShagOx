using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Common.Options.Systems.Cookies;
using ShagOxServer.Application.Interfaces.Services.Auth.Core;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Management.Moderation;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Devices.Management;
using CookieOptions = ShagOxServer.Application.Common.Options.Systems.Cookies.CookieOptions;

namespace ShagOxServer.Api.Controllers.Auth.Core.Admin;
[ApiController]
[Route("api/admin/auth")]
[Authorize(Roles = "Admin")]
public partial class AuthAdminController
    : ApiCookieController
{
    private readonly ILogoutService _logoutService;
    private readonly IUserModerationService _userModerationService;
    private readonly IDeviceModerationService _deviceModerationService;


    public AuthAdminController(
        ILogoutService logoutService,
        IUserModerationService userModerationService,
        IDeviceModerationService deviceModerationService,
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
        _userModerationService = userModerationService;
        _deviceModerationService = deviceModerationService;
    }
}
