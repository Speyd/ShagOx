using Microsoft.AspNetCore.Http;
using ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Interfaces.Providers;
using System.Net;
using UAParser;

namespace ShagOxServer.Infrastructure.ExternalServices.Auth;
public sealed class DeviceInfoProvider 
    : IDeviceInfoProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly Parser _parser = Parser.GetDefault();


    public DeviceInfoProvider(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }


    public UserDeviceInfo GetDeviceInfo(
        string deviceIdentifier)
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

        return new UserDeviceInfo(
            client.Device.Family,
            client.Device.Family,
            client.UA.Family,
            client.OS.Family,
            userIp?.ToString() ?? "",
            deviceIdentifier
        );
    }
}
