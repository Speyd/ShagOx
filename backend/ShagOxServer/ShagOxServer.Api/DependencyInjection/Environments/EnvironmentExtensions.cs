namespace ShagOxServer.Api.DependencyInjection.Environments;
public static partial class EnvironmentExtensions
{
    private const string EnvExtension = ".env";

    public static void LoadEnvironment(
        this IHostApplicationBuilder builder)
    {
        LoadRedisEnvironment(builder);
        LoadPostgresEnvironment(builder);
        LoadJwtEnvironment(builder);
    }
}
