using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Common.Options.Systems;
using ShagOxServer.Application.DTOs.Auth.Externals.Google;
using ShagOxServer.Application.Interfaces.Services.Auth.Externals;
using ShagOxServer.Application.Interfaces.Services.Common.Context;

namespace ShagOxServer.Api.Controllers.Auth.Core;

[ApiController]
[Route("api/auth/external")]
public class ExternalAuthController
    : ApiCookieController
{
    private readonly IGoogleLoginService _googleLoginService;


    public ExternalAuthController(
        IGoogleLoginService googleLoginService,
        IOptions<JwtOptions> jwtOptions,
        ILogger<ApiCookieController> logger,
        IUserContext userContext
    )
        : base(jwtOptions, logger, userContext)
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
