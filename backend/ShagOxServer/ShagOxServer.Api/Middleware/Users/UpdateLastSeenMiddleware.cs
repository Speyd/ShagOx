using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Systems.Middleware;
using ShagOxServer.Application.Interfaces.Providers.Devices;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Core;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices;
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
        IUserCommandsRepository userCommands,
        IUserDeviceCommandsRepository deviceCommands,
        IRefreshTokenService refreshTokenService)
    {

        if (context.User.Identity?.IsAuthenticated == true)
        {
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

            if (int.TryParse(userIdClaim?.Value, out var userId))
            {
                var device = await deviceProvier
                    .GetUserDeviceAsync(context.RequestAborted);
                
                var now = DateTime.UtcNow;
                var cutoff = now - _updaterOptions.UpdateLastSeen;

                await userCommands.UpdateLastSeenAsync(
                    userId,
                    context.RequestAborted);

                if (device is not null)
                {
                    await deviceCommands.UpdateLastActiveAsync(
                        device.Id,
                        context.RequestAborted);
                }
            }
        }

        await _next(context);
    }
}
