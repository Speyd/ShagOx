using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Common.Settings.Systems;
using ShagOxServer.Application.DTOs.Auth.Externals.Google;
using ShagOxServer.Application.Interfaces.Services.Auth.Externals;

namespace ShagOxServer.Api.Controllers.Auth;

[ApiController]
[Route("api/auth/external")]
public class ExternalAuthController
    : ApiCookieController
{
    private readonly IGoogleLoginService _googleLoginService;


    public ExternalAuthController(
        IGoogleLoginService googleLoginService,
        IOptions<JwtSettings> jwtSettings,
        ILogger<ApiCookieController> logger
    )
    : base(jwtSettings, logger)
    {
        _googleLoginService = googleLoginService;
    }

    [HttpPost("google")]
    public async Task<IActionResult> GoogleLogin(
        [FromBody] GoogleLoginRequest request)
    {    
        var responce = await _googleLoginService
            .LoginAsync(request);

        return await SetAccessToken(responce);
    }
}