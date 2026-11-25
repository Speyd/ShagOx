using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Systems.Middleware;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
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
        IUserContext userContext,
        IRefreshTokenService refreshTokenService)
    {

        if (context.User.Identity?.IsAuthenticated == true)
        {
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

            if (int.TryParse(userIdClaim?.Value, out var userId))
            {
                var deviceIdentifier = userContext.DeviceIdentifier;
                
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

                if (!string.IsNullOrWhiteSpace(deviceIdentifier))
                {
                    await dbContext.UserDevices
                        .Where(d =>
                            d.DeviceIdentifier == deviceIdentifier &&
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
