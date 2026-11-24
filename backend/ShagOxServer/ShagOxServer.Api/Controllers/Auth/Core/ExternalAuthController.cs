using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Common.Options.Systems.Cookies;
using ShagOxServer.Application.DTOs.Auth.Externals.Google;
using ShagOxServer.Application.Interfaces.Services.Auth.Externals;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using CookieOptions = ShagOxServer.Application.Common.Options.Systems.Cookies.CookieOptions;

namespace ShagOxServer.Api.Controllers.Auth.Core;

[ApiController]
[Route("api/auth/external")]
public class ExternalAuthController
    : ApiCookieController
{
    private readonly IGoogleLoginService _googleLoginService;


    public ExternalAuthController(
        IGoogleLoginService googleLoginService,
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
        _googleLoginService = googleLoginService;
    }

    [HttpPost("google")]
    public async Task<IActionResult> GoogleLogin(
        [FromBody] GoogleLoginRequest request)
    {
        var responce = await _googleLoginService
            .LoginAsync(request);

        return await SetTokens(responce);
    }
}
