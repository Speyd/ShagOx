using Microsoft.AspNetCore.Http;
using ShagOxServer.Application.DTOs.Devices;
using ShagOxServer.Application.Interfaces.Providers.Devices;
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


    public DeviceInfo GetDeviceInfo(
        string deviceIdentifier)
    {
        var context = _httpContextAccessor.HttpContext;

        var userAgent = context?.Request.Headers.UserAgent.ToString() ?? "";

        var client = _parser.Parse(userAgent);

        return new DeviceInfo(
            client.Device.Family,
            client.Device.Family,
            client.OS.Family,
            deviceIdentifier
        );
    }
}
