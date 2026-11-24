using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using System.Security.Claims;

namespace ShagOxServer.Api.Middleware.Users;
public class BlockedDeviceMiddleware
{
    private readonly RequestDelegate _next;


    public BlockedDeviceMiddleware(
        RequestDelegate next)
    {
        _next = next;
    }


    public async Task InvokeAsync(
        HttpContext context,
        AppDbContext dbContext,
        IUserContext userContext,
        IUserDeviceExistsRepository deviceExists)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

            if (int.TryParse(userIdClaim?.Value, out var userId))
            {
                var deviceIdentifier = userContext.DeviceIdentifier;

                if (!string.IsNullOrWhiteSpace(deviceIdentifier))
                {
                    var isBlocked = await deviceExists
                        .IsBlockedAsync(userId, deviceIdentifier);

                    if (isBlocked)
                    {
                        context.Response.StatusCode =
                            StatusCodes.Status403Forbidden;

                        await context.Response.WriteAsJsonAsync(
                            new
                            {
                                message = "This device has been blocked."
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
