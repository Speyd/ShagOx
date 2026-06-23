using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Auth;
using ShagOxServer.Application.DTOs.Auth.Register;
using ShagOxServer.Application.Interfaces;

namespace ShagOxServer.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;


    public AuthController(
        IAuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("register")]
    public async Task<ActionResult<RegisterResponse>> Register(
        RegisterRequest request)
    {
        var result =
            await _authService.RegisterAsync(request);

        return Ok(result);
    }
}