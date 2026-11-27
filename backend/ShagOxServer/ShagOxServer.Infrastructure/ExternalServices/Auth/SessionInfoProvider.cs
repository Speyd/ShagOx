using Microsoft.AspNetCore.Http;
using ShagOxServer.Application.DTOs.Auth.Users.UserSessions;
using ShagOxServer.Application.Interfaces.Providers.Sessions;
using System.Net;
using UAParser;

namespace ShagOxServer.Infrastructure.ExternalServices.Auth;
public sealed class SessionInfoProvider
    : ISessionInfoProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly Parser _parser = Parser.GetDefault();


    public SessionInfoProvider(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }


    public SessionInfo GetSessionInfo()
    {
        var context = _httpContextAccessor.HttpContext;

        var userAgent = context?.Request.Headers.UserAgent.ToString() ?? "";

        var client = _parser.Parse(userAgent);

        var cfIp = context?.Request.Headers["CF-Connecting-IP"]
                .ToString();

        IPAddress? userIp = null;

        if (IPAddress.TryParse(cfIp, out var parsedIp))
        {
            userIp = parsedIp;
        }
        else
        {
            userIp = context?.Connection.RemoteIpAddress;
        }

        var browserVersion = client.UA.Major is not null
            ? $"{client.UA.Major}.{client.UA.Minor}.{client.UA.Patch}"
            : "";

        return new SessionInfo(
            client.UA.Family,
            browserVersion,
            userIp?.ToString() ?? ""
        );
    }
}
