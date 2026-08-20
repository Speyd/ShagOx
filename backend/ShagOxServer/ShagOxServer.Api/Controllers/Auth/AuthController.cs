using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.DTOs.Auth.Register;
using ShagOxServer.Application.Interfaces.Services.Auth;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Auth;

[ApiController]
[Route("auth")]
public class AuthController : ApiController
{
    private readonly IRegisterService _registerService;
    private readonly ILoginService _loginService;


    public AuthController(
        IRegisterService registerService,
        ILoginService loginService)
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
        var result = await _loginService
            .LoginAsync(request);

        if (!result.IsSuccess)
            return Unauthorized(result.Error);

        if (result.Value?.Token is null)
            return StatusCode(StatusCodes.Status500InternalServerError);

        Response.Cookies.Append(
            "access_token",
            result.Value.Token,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });

        return Ok();
    }

    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("access_token");

        return Ok(new { message = "Logged out successfully" });
    }
}