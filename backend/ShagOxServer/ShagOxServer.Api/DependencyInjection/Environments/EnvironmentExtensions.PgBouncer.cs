using DotNetEnv;
using ShagOxServer.Api.DependencyInjection.Environments.Db;
using ShagOxServer.Application.Common.Options.Db.PgBouncer;
using ShagOxServer.SharedKernel.Configuration;

namespace ShagOxServer.Api.DependencyInjection.Environments;
public partial class EnvironmentExtensions
{
    private static void LoadPgBouncerEnvironment(
        IHostApplicationBuilder builder)
    {
        var envPath = EnvironmentFileLocator.Find(EnvExtension);

        Env.Load(envPath);

        var options = PgBouncerLoaderOptions.Load();

        builder.Configuration["ConnectionStrings:PrimaryConnection"] =
            PostgresConnectionStringBuilder.Build(
                options.User,
                options.Password,
                options.Database,
                options.PrimaryHost,
                options.PrimaryPort.ToString());

        for (var i = 0; i < options.Replicas.Count; i++)
        {
            var replica = options.Replicas[i];

            var connectionString = PostgresConnectionStringBuilder.Build(
                options.User,
                options.Password,
                replica.Database,
                options.ReplicaHost,
                replica.Port.ToString());


            builder.Configuration[
                $"ConnectionStrings:ReplicaConnections:{i}"] =
                connectionString;

            builder.Configuration[
                $"Postgres:ReplicaContainers:{i}"] =
                replica.ContainerName;
        }
    }
}
