using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Common.Settings.Systems;
using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.DTOs.Auth.Register;
using ShagOxServer.Application.Interfaces.Services.Auth;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Auth;

[ApiController]
[Route("api/auth")]
public class AuthController 
    : ApiCookieController
{
    private readonly IRegisterService _registerService;
    private readonly ILoginService _loginService;


    public AuthController(
        IRegisterService registerService,
        ILoginService loginService,
        IOptions<JwtSettings> jwtSettings
    )
    : base(jwtSettings)
    {
        _registerService = registerService;
        _loginService = loginService;
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

        return await SetAccessToken(responce);
    }

    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        DeleteAccessToken(Response);

        return Ok(new { message = "Logged out successfully" });
    }
}