using ShagOxServer.Application.Interfaces.Providers.Devices;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices;
using ShagOxServer.Application.Resources.Auth.Security;
using System.Security.Claims;

namespace ShagOxServer.Api.Middleware.Users;
public class BlockedUserDeviceMiddleware
{
    private readonly RequestDelegate _next;


    public BlockedUserDeviceMiddleware(
        RequestDelegate next)
    {
        _next = next;
    }


    public async Task InvokeAsync(
        HttpContext context,
        IDeviceProvider deviceProvider,
        IUserDeviceExistsRepository deviceExists)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

            if (int.TryParse(userIdClaim?.Value, out var userId))
            {
                var deviceIdentifier = await deviceProvider
                    .GetUserDeviceAsync(context.RequestAborted);

                var deviceId = deviceIdentifier?.Id;

                if (deviceId.HasValue)
                {
                    var isBlocked = await deviceExists
                        .IsBlockedAsync(userId, deviceId.Value,
                            context.RequestAborted);

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
                }
            }
        }

        await _next(context);
    }
}
