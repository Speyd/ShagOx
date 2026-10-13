using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Systems.Middleware;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Entities.Account;
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
        IRepository<User> repository,
        IQueryRepository<User> queryRepository,
        IUnitOfWork unitOfWork)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var userIdClaim =
                context.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim is not null)
            {
                var userId = int.Parse(userIdClaim.Value);

                var user = await queryRepository.GetByIdAsync(userId);

                var now = DateTime.UtcNow;

                if (user is not null && (user.LastSeenAt is null ||
                     now - user.LastSeenAt >= _settings.UpdateLastSeen))
                {
                    user.LastSeenAt = now;
                    repository.Update(user);
                    await unitOfWork.SaveChangesAsync();
                }
            }
        }

        await _next(context);
    }
}
