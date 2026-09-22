using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Auth.Google;
using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.DTOs.Auth.Register;
using ShagOxServer.Application.Interfaces.Services.Auth;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Auth;

[ApiController]
[Route("api/auth")]
public class AuthController : ApiController
{
    private readonly IRegisterService _registerService;
    private readonly ILoginService _loginService;
    private readonly IConfiguration _configuration;

    public AuthController(
        IRegisterService registerService,
        ILoginService loginService,
        IConfiguration configuration)
    {
        _registerService = registerService;
        _loginService = loginService;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _registerService.RegisterAsync(request);
        return result.ToActionResult();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _loginService.LoginAsync(request);

        if (!result.IsSuccess)
            return Unauthorized(result.Error);

        if (result.Value?.Token is null)
            return StatusCode(StatusCodes.Status500InternalServerError);

        SetAccessTokenCookie(result.Value.Token);

        return Ok();
    }

    [HttpPost("google")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Code))
            return BadRequest(new { message = "Google authorization code is empty." });

        var clientId = _configuration["Google:ClientId"];
        var clientSecret = _configuration["Google:ClientSecret"];

        if (string.IsNullOrWhiteSpace(clientId) ||
            string.IsNullOrWhiteSpace(clientSecret))
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "Google OAuth is not configured." });
        }

        using var httpClient = new HttpClient();

        var tokenResponse = await httpClient.PostAsync(
            "https://oauth2.googleapis.com/token",
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["code"] = request.Code,
                ["client_id"] = clientId,
                ["client_secret"] = clientSecret,
                ["redirect_uri"] = "postmessage",
                ["grant_type"] = "authorization_code"
            })
        );

        if (!tokenResponse.IsSuccessStatusCode)
        {
            var error = await tokenResponse.Content.ReadAsStringAsync();

            return BadRequest(new
            {
                message = "Google token exchange failed.",
                error
            });
        }

        var tokens = await tokenResponse.Content
            .ReadFromJsonAsync<GoogleTokenResponse>();

        if (tokens is null || string.IsNullOrWhiteSpace(tokens.IdToken))
            return BadRequest(new { message = "Google did not return an ID token." });

        GoogleJsonWebSignature.Payload payload;

        try
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[]
                {
                clientId
            }
            };

            payload = await GoogleJsonWebSignature.ValidateAsync(
                tokens.IdToken,
                settings
            );
        }
        catch (InvalidJwtException)
        {
            return BadRequest(new { message = "Invalid Google ID token." });
        }

        var result = await _loginService.GoogleLoginAsync(
            payload.Email,
            payload.Name,
            payload.Subject
        );

        if (!result.IsSuccess)
            return Unauthorized(result.Error);

        if (result.Value?.Token is null)
            return StatusCode(StatusCodes.Status500InternalServerError);

        SetAccessTokenCookie(result.Value.Token);

        return Ok();
    }

    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("access_token");
        return Ok(new { message = "Logged out successfully" });
    }

    private void SetAccessTokenCookie(string token)
    {
        Response.Cookies.Append(
            "access_token",
            token,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTimeOffset.UtcNow.AddDays(7),
                SameSite = SameSiteMode.Lax
            });
    }
}