using ShagOxServer.Application.DTOs.Devices.Create;
using ShagOxServer.Application.Interfaces.Providers.Devices;
using ShagOxServer.Application.Interfaces.Repositories.Devices;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Devices.Core.Create;
using ShagOxServer.Application.Resources.Auth.Security;

namespace ShagOxServer.Api.Middleware;
public class DeviceAccessMiddleware
{
    private readonly RequestDelegate _next;

    public DeviceAccessMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context,
        IUserContext userContext,
        IDeviceProvider deviceProvider,
        IDeviceCreateService deviceService,
        IDeviceCommandsRepository deviceCommands,
        IDeviceExistsRepository deviceExists)
    {
        var device = await deviceProvider.GetDeviceAsync();

        var deviceIdentifier = device?.DeviceIdentifier
            ?? userContext.DeviceIdentifier;

        if (!string.IsNullOrEmpty(deviceIdentifier))
        {
            var isBlocked = await deviceExists
                .IsBlockedAsync(deviceIdentifier);

            if (isBlocked)
            {
                context.Response.StatusCode =
                    StatusCodes.Status403Forbidden;

                await context.Response.WriteAsJsonAsync(
                    new
                    {
                        message = SecurityResources.DeviceBlocked
                    },
                    context.RequestAborted);

                return;
            }

            if (device is null)
            {
                await deviceService.CreateAsync(
                    new DeviceCreateRequest(deviceIdentifier));
            }
            else
            {
                await deviceCommands.UpdateLastActiveAsync(
                    device.Id);
            }
        }

        await _next(context);
    }
}
