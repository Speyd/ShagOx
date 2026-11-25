using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Systems.BackgroundServices.User;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices;

namespace ShagOxServer.Infrastructure.BackgroundServices.Auth;
public sealed class UserDeviceBanBackgroundService
    : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly UserBanOptions _options;
    private readonly ILogger<UserDeviceBanBackgroundService> _logger;

    public UserDeviceBanBackgroundService(
        IServiceScopeFactory scopeFactory,
        IOptions<UserBanOptions> options,
        ILogger<UserDeviceBanBackgroundService> logger
        )
    {
        _scopeFactory = scopeFactory;
        _options = options.Value;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(
            _options.WaitTimer);

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();

                var repository = scope.ServiceProvider
                    .GetRequiredService<IUserDeviceCommandsRepository>();

                await repository.UnbanExpiredDevicesAsync(stoppingToken);
            }
            catch (OperationCanceledException)
                when (stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation(
                    "Pending user device cleanup service stopped.");

                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, 
                    "Error processing expired user device bans");
            }
        }
    }
}

