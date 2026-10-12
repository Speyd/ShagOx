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

    private static string GetRequiredEnvironmentVariable(
        string name)
    {
        var value =
            Environment.GetEnvironmentVariable(name);

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException(
                $"Environment variable '{name}' is not configured.");
        }

        return value;
    }
}
