using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using System.Security.Claims;
using CookieOptions = ShagOxServer.Application.Common.Options.Systems.Cookies.CookieOptions;

namespace ShagOxServer.Application.Common.Context;
public class UserContext
    : IUserContext
{
    private readonly IHttpContextAccessor _http;
    private readonly CookieOptions _cookieOptions;

    public UserContext(
        IHttpContextAccessor http,
        IOptions<CookieOptions> cookieOptions)
    {
        _http = http;
        _cookieOptions = cookieOptions.Value;
    }

    public long? UserId
    {
        get
        {
            var value = _http.HttpContext?.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return long.TryParse(value, out var id) ? id : null;
        }
    }

    public string? DeviceIdentifier => 
        _http.HttpContext?
            .Request.Headers[_cookieOptions.DeviceIdentifierKey]
            .FirstOrDefault();

    public string? RefreshToken => 
        _http.HttpContext?
            .Request.Cookies[_cookieOptions.RefreshTokenKey];
}
