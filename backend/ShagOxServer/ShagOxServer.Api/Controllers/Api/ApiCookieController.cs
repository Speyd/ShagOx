using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Systems.Cookies;
using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;
using CookieOptions = ShagOxServer.Application.Common.Options.Systems.Cookies.CookieOptions;

namespace ShagOxServer.Api.Controllers.Api;
public abstract class ApiCookieController
    : ApiController
{
    private readonly CookieOptions _cookieOptions;
    private readonly JwtOptions _jwtOptions;
    private readonly RefreshTokenOptions _refreshOptions;

    private readonly ILogger<ApiCookieController> _logger;


    public ApiCookieController(
        IOptions<CookieOptions> cookieOptions,
        IOptions<JwtOptions> jwtOptions,
        IOptions<RefreshTokenOptions> refreshOptions,
        ILogger<ApiCookieController> logger,
        IUserContext userContext
    )
        : base(userContext)
    {
        _cookieOptions = cookieOptions.Value;
        _jwtOptions = jwtOptions.Value;
        _refreshOptions = refreshOptions.Value;
        _logger = logger;
    }


    protected void DeleteAccessToken(
        HttpResponse response)
    {
        response.Cookies.Delete(_cookieOptions.AccessTokenKey);

        _logger.LogInformation(
            "Refresh token cookie deleted successfully.");
    }

    protected void DeleteRefreshToken(
        HttpResponse response)
    {
        response.Cookies.Delete(_cookieOptions.RefreshTokenKey);

        _logger.LogInformation(
            "Access token cookie deleted successfully.");
    }

    protected void DeleteTokens(
        HttpResponse response)
    {
        DeleteAccessToken(response);

        DeleteRefreshToken(response);
    }


    protected async Task<IActionResult> SetTokens(
        Result<LoginResponse> response)
    {
        if (!response.IsSuccess || response.Value is null)
            return response.ToActionResult();

        await SetAccessTokenCookie(response);
        await SetRefreshTokenCookie(response);

        _logger.LogInformation("Authentication cookies set successfully.");

        return Ok(new
        {
            AccessToken = response.Value.AccessToken
        });
    }

    protected async Task<IActionResult> SetAccessTokenCookie(
        Result<LoginResponse> response)
    {
        if (!response.IsSuccess ||
            response.Value?.AccessToken is null)
            return response.ToActionResult();
        
        Response.Cookies.Append(
            _cookieOptions.AccessTokenKey,
            response.Value.AccessToken,
            new Microsoft.AspNetCore.Http.CookieOptions
            {
                HttpOnly = _cookieOptions.HttpOnly,
                Secure = _cookieOptions.Secure,
                Expires = DateTimeOffset.UtcNow
                    .AddMinutes(_jwtOptions.ExpireMinutes)
            });

        _logger.LogInformation(
            "Access token cookie set successfully.");

        return response.ToActionResult();
    }

    protected async Task<IActionResult> SetRefreshTokenCookie(
       Result<LoginResponse> response)
    {
        if (!response.IsSuccess ||
            response.Value?.RefreshToken is null)
            return response.ToActionResult();

        Response.Cookies.Append(
            _cookieOptions.RefreshTokenKey,
            response.Value.RefreshToken,
            new Microsoft.AspNetCore.Http.CookieOptions
            {
                HttpOnly = _cookieOptions.HttpOnly,
                Secure = _cookieOptions.Secure,
                Expires = DateTimeOffset.UtcNow
                    .AddDays(_refreshOptions.ExpireDays)
            });

        _logger.LogInformation(
            "Refresh token cookie set successfully.");

        return response.ToActionResult();
    }
}
