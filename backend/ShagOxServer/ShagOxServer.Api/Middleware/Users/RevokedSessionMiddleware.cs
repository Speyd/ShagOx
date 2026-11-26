using Microsoft.AspNetCore.Authorization;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserSessions;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Resources.Auth.Security;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using System.Security.Claims;

namespace ShagOxServer.Api.Middleware.Users;
public class RevokedSessionMiddleware
{
    private readonly RequestDelegate _next;


    public RevokedSessionMiddleware(
        RequestDelegate next)
    {
        _next = next;
    }


    public async Task InvokeAsync(
        HttpContext context,
        IUserContext userContext,
        IRefreshTokenService refreshTokenService,
        IUserSessionExistsRepository sessionExists)
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
                var refreshToken = userContext.RefreshToken;

                if (!string.IsNullOrWhiteSpace(refreshToken) && requiresAuthorization)
                {
                    var tokenHash = refreshTokenService.Hash(refreshToken);

                    var isRevokedSession = await sessionExists
                        .IsRevokedAsync(userId, tokenHash);

                    if (isRevokedSession)
                    {
                        context.Response.StatusCode =
                            StatusCodes.Status403Forbidden;

                        await context.Response.WriteAsJsonAsync(
                            new
                            {
                                message = SecurityResources.SessionRevoked
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
