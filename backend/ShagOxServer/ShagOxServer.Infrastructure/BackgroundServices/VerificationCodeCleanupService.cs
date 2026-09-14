using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShagOxServer.Infrastructure.Persistence.DbContexts;

namespace ShagOxServer.Api.BackgroundServices;
public class VerificationCodeCleanupService 
    : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public VerificationCodeCleanupService(
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

                var expiredCodes = await db.VerificationCodes
                    .Where(x =>
                        x.ExpiresAt <= DateTime.UtcNow ||
                        x.UsedAt != null ||
                        x.InvalidatedAt != null)
                    .ToListAsync(stoppingToken);

                if (expiredCodes.Count > 0)
                {
                    db.VerificationCodes.RemoveRange(expiredCodes);

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