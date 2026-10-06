namespace ShagOxServer.Api.DependencyInjection.Environments;
public static partial class EnvironmentExtensions
{
    private const string RedisDataPath = "infrastructure/my_redis";
    private const string DataBasePath = "postgres-replication";

    private const string EnvExtension = ".env";

    public static void LoadEnvironment(
        this IHostApplicationBuilder builder)
    {
        LoadRedisEnvironment(builder);
        LoadPostgresEnvironment(builder);
    }
}