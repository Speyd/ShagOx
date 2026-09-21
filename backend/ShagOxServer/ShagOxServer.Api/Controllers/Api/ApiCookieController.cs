using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Systems;
using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.Interfaces.Services.Auth.Cookies;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Api;
public abstract class ApiCookieController
    : ApiController, IAuthCookieService
{
    private readonly CookieSettings _cookieSettings;


    public ApiCookieController(
        IOptions<CookieSettings> jwtSettings)
    {
        _cookieSettings = jwtSettings.Value;
    }


    public void DeleteAccessToken(
        HttpResponse response)
    {
        response.Cookies.Delete(_cookieSettings.CookieKey);
    }

    public async Task<IActionResult> SetAccessToken(
        Result<LoginResponse> response)
    {
        if (!response.IsSuccess ||
            response.Value?.Token is null)
            return response.ToActionResult();

        Response.Cookies.Append(
            _cookieSettings.CookieKey,
            response.Value.Token,
            new CookieOptions
            {
                HttpOnly = _cookieSettings.CookieHttpOnly,
                Secure = _cookieSettings.CookieSecure,
                Expires = DateTimeOffset.UtcNow
                    .AddDays(_cookieSettings.CookieExpireDays)
            });

        return response.ToActionResult();
    }
}