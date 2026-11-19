using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Api;
public abstract class ApiCookieController
    : ApiController
{
    private readonly Application.Common.Options.Systems.CookieOptions _cookieOptions;

    private readonly ILogger<ApiCookieController> _logger;


    public ApiCookieController(
        IOptions<Application.Common.Options.Systems.CookieOptions> jwtOptions,
        ILogger<ApiCookieController> logger,
        IUserContext userContext
    )
        : base(userContext)
    {
        _cookieOptions = jwtOptions.Value;
        _logger = logger;
    }


    protected void DeleteAccessToken(
        HttpResponse response)
    {
        response.Cookies.Delete(_cookieOptions.CookieKey);

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
            _cookieOptions.CookieKey,
            response.Value.Token,
            new Microsoft.AspNetCore.Http.CookieOptions
            {
                HttpOnly = _cookieOptions.CookieHttpOnly,
                Secure = _cookieOptions.CookieSecure,
                Expires = DateTimeOffset.UtcNow
                    .AddDays(_cookieOptions.CookieExpireDays)
            });

        _logger.LogInformation(
            "Access token cookie set successfully.");

        return response.ToActionResult();
    }
}
