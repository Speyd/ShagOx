using Microsoft.Extensions.Options;
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

        var settings = serviceProvider
            .GetRequiredService<IOptions<RedisSettings>>()
            .Value;

        if (settings is null)
        {
            throw new InvalidOperationException(
                "Redis settings are not configured.");
        }

        var options = ConfigurationOptions.Parse(
            settings.Endpoints);

        options.Ssl = settings.Ssl;
        options.User = settings.User;
        options.Password = settings.Password;

        var certRoot = Path.Combine(ProjectPath.Root,
            settings.CertificatePath);

        //options.SetUserPemCertificate(
        //    userCertificatePath: Path.Combine(
        //        certRoot,
        //        settings.ClientCrtFileName),
        //    userKeyPath: Path.Combine(
        //        certRoot,
        //        settings.ClientKeyFileName));

        //options.TrustIssuer(
        //    Path.Combine(
        //        certRoot,
        //        settings.CaFileName));

        options.TrustIssuer(
            Path.Combine(
                certRoot,
                settings.CaFileName));

        var muxer = await ConnectionMultiplexer
            .ConnectAsync(options);

        return services.AddSingleton<IConnectionMultiplexer>(muxer);
    }
}