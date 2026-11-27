using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Common.Options.Systems.Cookies;
using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.DTOs.Auth.Register;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
using ShagOxServer.Application.Interfaces.Services.Auth;
using ShagOxServer.Application.Interfaces.Services.Auth.Core;
using ShagOxServer.Application.Interfaces.Services.Auth.Core.Login;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts.TwoFactor;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Management.Trast;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions.Refresh;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;
using CookieOptions = ShagOxServer.Application.Common.Options.Systems.Cookies.CookieOptions;

namespace ShagOxServer.Api.Controllers.Auth.Core;

[ApiController]
[Route("api/auth")]
public partial class AuthController 
    : ApiCookieController
{
    private readonly IRegisterService _registerService;
    private readonly ILoginService _loginService;
    private readonly ILogoutService _logoutService;
    private readonly IUserSessionRefreshService _refreshService;
    private readonly IUserTrastManagementService _trastManagement;
    private readonly ITwoFactorService _twoFactor;


    public AuthController(
        IUserQueryRepository userRepository,
        IRegisterService registerService,
        ILoginService loginService,
        ILogoutService logoutService,
        IUserSessionRefreshService refreshService,
        IUserTrastManagementService trastManagement,
        ITwoFactorService twoFactor,
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
        _registerService = registerService;
        _loginService = loginService;
        _logoutService = logoutService;
        _refreshService = refreshService;
        _trastManagement = trastManagement;
        _twoFactor = twoFactor;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request)
    {
        var result = await _registerService
            .RegisterAsync(request);

        return result.ToActionResult();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request)
    {
        var responce = await _loginService
            .LoginAsync(request);

        return await SetTokens(responce);
    }

    [HttpPost("refresh")]
    [Authorize]
    public async Task<IActionResult> Refresh(
        CancellationToken cancellationToken)
    {
        var refreshToken = UserContext.RefreshToken;

        if (string.IsNullOrWhiteSpace(refreshToken))
            return Unauthorized();

        var responce = await _refreshService.RefreshAsync(
            refreshToken,
            cancellationToken);

        if (!responce.IsSuccess)
            return Unauthorized();

        return await SetTokens(responce);
    }
}
