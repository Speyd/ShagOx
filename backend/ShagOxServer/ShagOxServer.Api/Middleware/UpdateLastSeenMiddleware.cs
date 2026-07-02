using ShagOxServer.Infrastructure.Interfaces.Auth.Users;
using System.Security.Claims;

namespace ShagOxServer.Api.Middleware;

public class UpdateLastSeenMiddleware
{
    private readonly RequestDelegate _next;

    public UpdateLastSeenMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context,
        IUserRepository repository)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var userIdClaim =
                context.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim is not null)
            {
                var userId = int.Parse(userIdClaim.Value);

                var user = await repository.GetByIdAsync(userId);

                if (user is not null)
                {
                    user.LastSeenAt = DateTime.UtcNow;
                    await repository.UpdateAsync(user);
                }
            }
        }

        await _next(context);
    }
}