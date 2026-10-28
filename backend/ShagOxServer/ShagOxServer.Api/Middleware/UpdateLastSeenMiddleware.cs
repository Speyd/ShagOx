using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Systems.Middleware;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using System.Security.Claims;

namespace ShagOxServer.Api.Middleware;
public class UpdateLastSeenMiddleware
{
    private readonly UpdateLastSeenSettings _settings;
    private readonly RequestDelegate _next;

  
    public UpdateLastSeenMiddleware(
        IOptions<UpdateLastSeenSettings> settings,
        RequestDelegate next)
    {
        _next = next;
        _settings = settings.Value;
    }

    public async Task InvokeAsync(
        HttpContext context,
        AppDbContext dbContext)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

            if (int.TryParse(userIdClaim?.Value, out var userId))
            {
                var now = DateTime.UtcNow;
                var cutoff = now - _settings.UpdateLastSeen;

                await dbContext.Users
                    .Where(u => u.Id == userId &&
                        (u.LastSeenAt == null ||
                         u.LastSeenAt <= cutoff))
                    .ExecuteUpdateAsync(
                        setters => setters.SetProperty(
                            u => u.LastSeenAt,
                            now),
                        context.RequestAborted);
            }
        }

        await _next(context);
    }
}
