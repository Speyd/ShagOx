using ShagOxServer.Api.Common;
using ShagOxServer.Application.Common.Options.Caches;
using StackExchange.Redis;

namespace ShagOxServer.Api.DependencyInjection;
public static class CacheExtensions
{
    public static async Task<IServiceCollection> AddCache(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<RedisOptions>(
            configuration.GetSection("Redis"));

        services.Configure<RedisOptions>(
            configuration.GetSection("UserRedis"));

        services.Configure<CacheOptions>(
            configuration.GetSection("Cache"));

        var dataOptions = configuration
            .GetSection("Redis")
            .Get<RedisOptions>()
            ?? throw new InvalidOperationException(
                "Redis options are not configured.");

        var userOptions = configuration
            .GetSection("UserRedis")
            .Get<RedisOptions>()
            ?? throw new InvalidOperationException(
                "UserRedis options are not configured.");

        var dataMuxer = await CreateConnectionAsync(dataOptions);
        var userMuxer = await CreateConnectionAsync(userOptions);

        services.AddKeyedSingleton<IConnectionMultiplexer>(
            "DataRedis", dataMuxer);

        services.AddKeyedSingleton<IConnectionMultiplexer>(
            "UserRedis", userMuxer);

        return services;
    }

    private static async Task<IConnectionMultiplexer>
        CreateConnectionAsync(RedisOptions options)
    {
        var redisOptions = ConfigurationOptions.Parse(
            options.Endpoints);

        redisOptions.Ssl = options.Ssl;
        redisOptions.User = options.User;
        redisOptions.Password = options.Password;

        if (options.Ssl)
        {
            var clientCertRoot = Path.Combine(
                ProjectPath.Root,
                options.ClientPath);

            redisOptions.SetUserPemCertificate(
                userCertificatePath: Path.Combine(
                    clientCertRoot,
                    options.ClientCrtFileName),
                userKeyPath: Path.Combine(
                    clientCertRoot,
                    options.ClientKeyFileName));

            var caCertRoot = Path.Combine(
                ProjectPath.Root,
                options.CaPath);

            redisOptions.TrustIssuer(
                Path.Combine(
                    caCertRoot,
                    options.CaFileName));
        }

        return await ConnectionMultiplexer.ConnectAsync(redisOptions);
    }
}
