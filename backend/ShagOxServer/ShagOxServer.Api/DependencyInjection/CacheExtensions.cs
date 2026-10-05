using ShagOxServer.Api.Common;
using ShagOxServer.Application.Common.Settings.Caches;
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

        var serviceProvider = services.BuildServiceProvider();

        var settings = configuration
            .GetSection("Redis")
            .Get<RedisSettings>()
            ?? throw new InvalidOperationException(
                "Redis settings are not configured.");

        var options = ConfigurationOptions.Parse(
            settings.Endpoints);

        

        options.Ssl = settings.Ssl;
        options.User = settings.User;
        options.Password = settings.Password;

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
    

        var muxer = await ConnectionMultiplexer
            .ConnectAsync(options);

        return services.AddSingleton<IConnectionMultiplexer>(muxer);
    }
}