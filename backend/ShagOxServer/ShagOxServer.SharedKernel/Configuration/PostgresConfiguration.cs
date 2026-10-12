using DotNetEnv;
using Npgsql;

namespace ShagOxServer.SharedKernel.Configuration;
public static class PostgresConfiguration
{
    public static void LoadEnvironment(string envPath)
    {
        if (!File.Exists(envPath))
        {
            throw new FileNotFoundException(
            $"Environment file not found: {envPath}");
        }

        Env.Load(envPath);
    }

    public static string BuildPrimaryConnectionString()
    {
        return new NpgsqlConnectionStringBuilder
        {
            Host = Required("POSTGRES_PRIMARY_HOST"),
            Port = int.Parse(Required("POSTGRES_PRIMARY_PORT")),
            Database = Required("POSTGRES_DB"),
            Username = Required("POSTGRES_USER"),
            Password = Required("POSTGRES_PASSWORD"),
            SslMode = SslMode.Disable
        }.ConnectionString;
    }

    private static string Required(string name)
    {
        var value = Environment.GetEnvironmentVariable(name);

        return !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new InvalidOperationException(
                $"Environment variable '{name}' is not configured.");
    }
}
