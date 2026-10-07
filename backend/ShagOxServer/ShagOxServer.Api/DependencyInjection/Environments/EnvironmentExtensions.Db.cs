using DotNetEnv;
using ShagOxServer.Api.Common;

namespace ShagOxServer.Api.DependencyInjection.Environments;
public partial class EnvironmentExtensions
{
    private static void LoadPostgresEnvironment(
        IHostApplicationBuilder builder)
    {
        var databaseEnvPath = Path.Combine(
            ProjectPath.Root,
            EnvExtension);

        Env.Load(databaseEnvPath);

        var user =
            GetRequiredEnvironmentVariable("POSTGRES_USER");

        var password =
            GetRequiredEnvironmentVariable("POSTGRES_PASSWORD");

        var database =
            GetRequiredEnvironmentVariable("POSTGRES_DB");

        var primaryPort =
            GetRequiredEnvironmentVariable("POSTGRES_PRIMARY_PORT");

        var replicaPort =
            GetRequiredEnvironmentVariable("POSTGRES_REPLICA_PORT");

        builder.Configuration["ConnectionStrings:PrimaryConnection"] =
            BuildPostgresConnectionString(
                user,
                password,
                database,
                primaryPort);

        builder.Configuration["ConnectionStrings:ReplicaConnection"] =
            BuildPostgresConnectionString(
                user,
                password,
                database,
                replicaPort);
    }

    private static string BuildPostgresConnectionString(
        string user,
        string password,
        string database,
        string port)
    {
        return
            $"Host=localhost;" +
            $"Port={port};" +
            $"Database={database};" +
            $"Username={user};" +
            $"Password={password};" +
            $"SSL Mode=Disable";
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