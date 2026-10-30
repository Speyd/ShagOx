using ShagOxServer.Api.Common;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches;
using StackExchange.Redis;

namespace ShagOxServer.Api.DependencyInjection;
public static class CacheExtensions
{
    public static async Task<IServiceCollection> AddCache(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<RedisSettings>(
            configuration.GetSection("Redis"));

        services.Configure<RedisSettings>(
            configuration.GetSection("UserRedis"));

        services.Configure<CacheSettings>(
            configuration.GetSection("Cache"));

        var dataSettings = configuration
            .GetSection("Redis")
            .Get<RedisSettings>()
            ?? throw new InvalidOperationException(
                "Redis settings are not configured.");

        var userSettings = configuration
            .GetSection("UserRedis")
            .Get<RedisSettings>()
            ?? throw new InvalidOperationException(
                "UserRedis settings are not configured.");

        var dataMuxer = await CreateConnectionAsync(dataSettings);
        var userMuxer = await CreateConnectionAsync(userSettings);

        services.AddKeyedSingleton<IConnectionMultiplexer>(
            "DataRedis", dataMuxer);

        services.AddKeyedSingleton<IConnectionMultiplexer>(
            "UserRedis", userMuxer);

        return services;
    }

    private static async Task<IConnectionMultiplexer>
        CreateConnectionAsync(RedisSettings settings)
    {
        var options = ConfigurationOptions.Parse(
            settings.Endpoints);

        options.Ssl = settings.Ssl;
        options.User = settings.User;
        options.Password = settings.Password;

        if (settings.Ssl)
        {
            var clientCertRoot = Path.Combine(
                ProjectPath.Root,
                settings.ClientPath);

            options.SetUserPemCertificate(
                userCertificatePath: Path.Combine(
                    clientCertRoot,
                    settings.ClientCrtFileName),
                userKeyPath: Path.Combine(
                    clientCertRoot,
                    settings.ClientKeyFileName));

            var caCertRoot = Path.Combine(
                ProjectPath.Root,
                settings.CaPath);

            options.TrustIssuer(
                Path.Combine(
                    caCertRoot,
                    settings.CaFileName));
        }

        return await ConnectionMultiplexer.ConnectAsync(options);
    }
}
