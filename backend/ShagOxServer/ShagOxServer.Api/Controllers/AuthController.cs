using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.DTOs.Auth.Register;
using ShagOxServer.Application.Interfaces.Auth;
using ShagOxServer.Application.Common.Results.Extensions;

namespace ShagOxServer.Api.Controllers;

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
        RegisterRequest request)
    {
        var result = await _registerService.RegisterAsync(request);

        return result.ToActionResult();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginRequest request)
    {
        var result = await _loginService.LoginAsync(request);

        return result.ToActionResult();
    }
}