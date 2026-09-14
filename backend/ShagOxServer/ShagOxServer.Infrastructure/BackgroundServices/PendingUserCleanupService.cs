using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShagOxServer.Domain.Entities.Account.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts;

namespace ShagOxServer.Infrastructure.BackgroundServices;
public class PendingUserCleanupService
    : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public PendingUserCleanupService(
        IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();

                var db = scope.ServiceProvider
                    .GetRequiredService<AppDbContext>();

                var expirationDate = DateTime.UtcNow.AddHours(-24);

                var pendingUsers = await db.Users
                    .Where(x =>
                        x.Status == UserStatus.PendingVerification &&
                        x.RegisteredAt <= expirationDate)
                    .ToListAsync(stoppingToken);

                if (pendingUsers.Count > 0)
                {
                    db.Users.RemoveRange(pendingUsers);

                    await db.SaveChangesAsync(stoppingToken);
                }
            }
            catch (Exception ex)
            {
            }

            await Task.Delay(
                TimeSpan.FromHours(1),
                stoppingToken);
        }
    }
}