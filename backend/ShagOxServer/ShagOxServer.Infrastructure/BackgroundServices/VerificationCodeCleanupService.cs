using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings;
using ShagOxServer.Infrastructure.Persistence.DbContexts;

namespace ShagOxServer.Api.BackgroundServices;
public class VerificationCodeCleanupService 
    : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    private readonly BackgroundServiceSettings _setting;


    public VerificationCodeCleanupService(
        IServiceScopeFactory scopeFactory,
        IOptions<BackgroundServiceSettings> setting)
    {
        _scopeFactory = scopeFactory;
        _setting = setting.Value;
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
                _setting.VerificationCodeCleanupInterval,
                stoppingToken
            );
        }
    }
}