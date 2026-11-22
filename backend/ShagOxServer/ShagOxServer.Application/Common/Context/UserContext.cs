using Microsoft.AspNetCore.Http;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using System.Security.Claims;

namespace ShagOxServer.Application.Common.Context;
public class UserContext
    : IUserContext
{
    private readonly IHttpContextAccessor _http;

    public UserContext(IHttpContextAccessor http)
    {
        _http = http;
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
}
