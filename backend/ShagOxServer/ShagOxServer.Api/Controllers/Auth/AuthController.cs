using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.DTOs.Auth.Register;
using ShagOxServer.Application.Interfaces.Auth;
using ShagOxServer.SharedKernel.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Auth;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
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
        var result = await _registerService.RegisterAsync(request);

        return result.ToActionResult();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request)
    {
        var result = await _loginService.LoginAsync(request);

        if (!result.IsSuccess || result.Value?.Token is null)
            return BadRequest(result.Error);

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
    public IActionResult Logout()
    {
        Response.Cookies.Delete("access_token");

        return Ok(new { message = "Logged out successfully" });
    }
}