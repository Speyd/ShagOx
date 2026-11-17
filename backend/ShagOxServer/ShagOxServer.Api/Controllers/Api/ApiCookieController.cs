using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Systems;
using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Api;
public abstract class ApiCookieController
    : ApiController
{
    private readonly Application.Common.Options.Systems.CookieOptions _cookieSettings;

    private readonly ILogger<ApiCookieController> _logger;


    public ApiCookieController(
        IOptions<Application.Common.Options.Systems.CookieOptions> jwtSettings,
        ILogger<ApiCookieController> logger,
        IUserContext userContext
    )
        : base(userContext)
    {
        _cookieSettings = jwtSettings.Value;
        _logger = logger;
    }


    protected void DeleteAccessToken(
        HttpResponse response)
    {
        response.Cookies.Delete(_cookieSettings.CookieKey);

        _logger.LogInformation(
            "Access token cookie deleted successfully.");
    }

    protected async Task<IActionResult> SetAccessToken(
        Result<LoginResponse> response)
    {
        if (!response.IsSuccess ||
            response.Value?.Token is null)
            return response.ToActionResult();

        Response.Cookies.Append(
            _cookieSettings.CookieKey,
            response.Value.Token,
            new Microsoft.AspNetCore.Http.CookieOptions
            {
                HttpOnly = _cookieSettings.CookieHttpOnly,
                Secure = _cookieSettings.CookieSecure,
                Expires = DateTimeOffset.UtcNow
                    .AddDays(_cookieSettings.CookieExpireDays)
            });

        _logger.LogInformation(
            "Access token cookie set successfully.");

        return response.ToActionResult();
    }
}
