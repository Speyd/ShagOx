using Microsoft.AspNetCore.Http;
using ShagOxServer.Application.Interfaces.Common.Context;
using System.Security.Claims;

namespace ShagOxServer.Application.Common.Context;
public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _http;

    public UserContext(IHttpContextAccessor http)
    {
        _http = http;
    }

    public int UserId =>
        int.Parse(_http.HttpContext!.User
            .FindFirst(ClaimTypes.NameIdentifier)!.Value);
}