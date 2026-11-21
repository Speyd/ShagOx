namespace ShagOxServer.Application.Common.Options.Db.Postgres;
public static class PostgresLoaderOptions
{
    public static PostgresOptions Load()
    {
        var containerNames = Required("POSTGRES_REPLICA_CONTAINER_NAMES")
            .Split(
                ',',
                StringSplitOptions.RemoveEmptyEntries |
                StringSplitOptions.TrimEntries);

        var replicas = containerNames
            .Select((name, index) => new PostgresReplicaOptions
            {
                ContainerName = name,
                Port = int.Parse(
                    Required($"POSTGRES_REPLICA_{index + 1}_PORT"))
            })
            .ToArray();

        return new PostgresOptions
        {
            User = Required("POSTGRES_USER"),
            Password = Required("POSTGRES_PASSWORD"),
            Database = Required("POSTGRES_DB"),

            PrimaryHost = Required("POSTGRES_PRIMARY_HOST"),
            PrimaryPort = int.Parse(
                Required("POSTGRES_PRIMARY_PORT")),

            ReplicaHost = Required("POSTGRES_REPLICA_HOST"),
            Replicas = replicas
        };
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
