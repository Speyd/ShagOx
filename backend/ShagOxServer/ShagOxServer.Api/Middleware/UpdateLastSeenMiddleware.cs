using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Systems.Middleware;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using System.Security.Claims;

namespace ShagOxServer.Api.Middleware;
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
        var endpoint = context.GetEndpoint();

        var requiresAuthorization = endpoint?
            .Metadata
            .GetOrderedMetadata<IAuthorizeData>()
            .Any() == true;

        if (context.User.Identity?.IsAuthenticated == true)
        {
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

            if (int.TryParse(userIdClaim?.Value, out var userId))
            {
                var deviceIdentifier = userContext.DeviceIdentifier;
                var refreshToken = userContext.RefreshToken;

                if (!string.IsNullOrWhiteSpace(deviceIdentifier))
                {
                    var isBlocked = await dbContext.UserDevices
                        .AsNoTracking()
                        .AnyAsync(
                            d => d.OwnerId == userId &&
                                 d.DeviceIdentifier == deviceIdentifier &&
                                 d.Status == UserDeviceStatus.Blocked,
                            context.RequestAborted);

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

                if (!string.IsNullOrWhiteSpace(refreshToken) && requiresAuthorization)
                {
                    var tokenHash = refreshTokenService.Hash(refreshToken);

                    var isRevokedSession = await dbContext.UserSessions
                        .AsNoTracking()
                        .AnyAsync(
                            d => d.OwnerId == userId &&
                                 d.RefreshTokenHash == tokenHash &&
                                 d.RevokedAt != null,
                            context.RequestAborted);

                    if (isRevokedSession)
                    {
                        context.Response.StatusCode =
                            StatusCodes.Status403Forbidden;

                        await context.Response.WriteAsJsonAsync(
                            new
                            {
                                message = "This session has been revoked."
                            },
                            context.RequestAborted);

                        return;
                    }
                }

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
