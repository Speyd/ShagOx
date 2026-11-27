using Microsoft.AspNetCore.Authorization;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserSessions;
using ShagOxServer.Application.Resources.Auth.Logins;

namespace ShagOxServer.Api.Middleware.Users;
public class BlockedSessionMiddleware
{
    private readonly RequestDelegate _next;

    public BlockedSessionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context,
        IUserSessionCommandsRepository sessionCommands,
        IUserSessionExistsRepository sessionExists)
    {
        var endpoint = context.GetEndpoint();

        var requiresAuthorization = endpoint?
            .Metadata
            .GetOrderedMetadata<IAuthorizeData>()
            .Any() == true;

        if (context.User.Identity?.IsAuthenticated == true)
        {
            var sessionIdClaim = context.User.FindFirst("sid");

            if (int.TryParse(sessionIdClaim?.Value, out var sessionId) &&
                requiresAuthorization)
            {
                var isActive = await sessionExists.IsActiveAsync(
                    sessionId,
                    context.RequestAborted);

                if (!isActive)
                {
                    context.Response.StatusCode =
                        StatusCodes.Status401Unauthorized;

                    await context.Response.WriteAsJsonAsync(
                        new
                        {
                            message = LoginAuthResources.SessionInactiveOrExpired
                        },
                        context.RequestAborted);

                    return;
                }

                await sessionCommands
                    .UpdateLastUsedAsync(sessionId, context.RequestAborted);
            }
        }

        await _next(context);
    }
}
