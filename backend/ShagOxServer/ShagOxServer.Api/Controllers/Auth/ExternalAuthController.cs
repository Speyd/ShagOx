using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Auth.Externals.Google;
using ShagOxServer.Application.Interfaces.Services.Auth.Externals;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Auth;
[ApiController]
[Route("api/auth/external")]
public class ExternalAuthController
    : ApiController
{
    private readonly IGoogleLoginService _googleLoginService;


    public ExternalAuthController(
        IGoogleLoginService googleLoginService)
    {
        _googleLoginService = googleLoginService;
    }

    [HttpPost("google")]
    public async Task<IActionResult> GoogleLogin(
        [FromBody] GoogleLoginRequest request)
    {
        
        var result = await _googleLoginService
            .LoginAsync(request);

        if (!result.IsSuccess)
            return result.ToActionResult();

        if (result.Value?.Token is null)
            return StatusCode(StatusCodes.Status500InternalServerError);

        Response.Cookies.Append(
            "access_token",
            result.Value.Token,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTimeOffset.UtcNow.AddDays(7),
                SameSite = SameSiteMode.Lax
            });

        return Ok();
    }
}