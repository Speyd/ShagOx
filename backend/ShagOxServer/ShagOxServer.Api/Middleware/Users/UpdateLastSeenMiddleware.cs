using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Systems.Middleware;
using ShagOxServer.Application.Interfaces.Providers.Devices;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using System.Security.Claims;

namespace ShagOxServer.Api.Middleware.Users;
public class UpdateLastSeenMiddleware
{
    private readonly UpdateLastSeenOptions _updaterOptions;
    private readonly RequestDelegate _next;


    public UpdateLastSeenMiddleware(
        IOptions<UpdateLastSeenOptions> updaterOptions,
        RequestDelegate next)
    {
        _next = next;
        _updaterOptions = updaterOptions.Value;
    }


    public async Task InvokeAsync(
        HttpContext context,
        AppDbContext dbContext,
        IDeviceProvider deviceProvier,
        IRefreshTokenService refreshTokenService)
    {

        if (context.User.Identity?.IsAuthenticated == true)
        {
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

            if (int.TryParse(userIdClaim?.Value, out var userId))
            {
                var device = await deviceProvier
                    .GetDeviceAsync();
                
                var now = DateTime.UtcNow;
                var cutoff = now - _updaterOptions.UpdateLastSeen;

                await dbContext.Users
                    .Where(u => u.Id == userId &&
                        (u.LastSeenAt == null ||
                         u.LastSeenAt <= cutoff))
                    .ExecuteUpdateAsync(
                        setters => setters.SetProperty(
                            u => u.LastSeenAt,
                            now),
                        context.RequestAborted);

                if (device is not null)
                {
                    await dbContext.UserDevices
                        .Where(d =>
                            d.DeviceId == device.Id &&
                            d.OwnerId == userId)
                        .ExecuteUpdateAsync(
                            setters => setters.SetProperty(
                                d => d.LastActiveAt,
                                now),
                            context.RequestAborted);
                }
            }
        }

        await _next(context);
    }
}
