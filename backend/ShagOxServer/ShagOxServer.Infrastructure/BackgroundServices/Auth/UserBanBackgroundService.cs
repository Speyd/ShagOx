using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Core;

namespace ShagOxServer.Infrastructure.BackgroundServices.Auth;
public sealed class UserBanBackgroundService 
    : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<UserBanBackgroundService> _logger;

    public UserBanBackgroundService(
        IServiceScopeFactory scopeFactory,
        ILogger<UserBanBackgroundService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(
            TimeSpan.FromMinutes(1));

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();

                var repository = scope.ServiceProvider
                    .GetRequiredService<IUserCommandsRepository>();

                await repository.UnbanExpiredUsersAsync(stoppingToken);
            }
            catch (OperationCanceledException)
                when (stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation(
                    "Pending user cleanup service stopped.");

                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing expired user bans");
            }
        }
    }
}
