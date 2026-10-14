using DotNetEnv;
using ShagOxServer.Application.Common.Settings.Db.Postgres;
using ShagOxServer.SharedKernel.Configuration;

namespace ShagOxServer.Api.DependencyInjection.Environments;
public partial class EnvironmentExtensions
{
    private static void LoadPostgresEnvironment(
        IHostApplicationBuilder builder)
    {
        var envPath = EnvironmentFileLocator.Find(EnvExtension);

        Env.Load(envPath);

        var settings = PostgresSettingsLoader.Load();

        builder.Configuration["ConnectionStrings:PrimaryConnection"] =
            BuildPostgresConnectionString(
                settings.User,
                settings.Password,
                settings.Database,
                settings.PrimaryHost,
                settings.PrimaryPort.ToString());

        for (var i = 0; i < settings.Replicas.Count; i++)
        {
            var replica = settings.Replicas[i];

            var connectionString = BuildPostgresConnectionString(
                settings.User,
                settings.Password,
                settings.Database,
                settings.ReplicaHost,
                replica.Port.ToString());

            builder.Configuration[
                $"ConnectionStrings:ReplicaConnections:{i}"] =
                connectionString;

            builder.Configuration[
                $"Postgres:ReplicaContainers:{i}"] =
                replica.ContainerName;
        }
    }

    private static string BuildPostgresConnectionString(
        string user,
        string password,
        string database,
        string host,
        string port)
    {
        return
            $"Host={host};" +
            $"Port={port};" +
            $"Database={database};" +
            $"Username={user};" +
            $"Password={password};" +
            $"SSL Mode=Disable";
    }
}
