namespace ShagOxServer.Application.Common.Settings.Db.PgBouncer;
public class PgBouncerSettingsLoader
{
    public static PgBouncerSettings Load()
    {
        var databases = Required("PGBOUNCER_REPLICA_DATABASES")
            .Split(
                ',',
                StringSplitOptions.RemoveEmptyEntries |
                StringSplitOptions.TrimEntries);

        var containers = Required("POSTGRES_REPLICA_CONTAINER_NAMES")
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (containers.Length != databases.Length)
            throw new InvalidOperationException(
                "POSTGRES_REPLICA_CONTAINER_NAMES and PGBOUNCER_REPLICA_DATABASES must have the same number of items.");

        var replicas = databases
            .Select((name, index) => new PgBouncerReplicaSettings
            {
                Database = name,
                ContainerName = containers[index],
                Port = int.Parse(
                    Required($"PGBOUNCER_PORT"))
            })
            .ToArray();

        return new PgBouncerSettings
        {
            User = Required("PGBOUNCER_USER"),
            Password = Required("PGBOUNCER_PASSWORD"),
            Database = Required("PGBOUNCER_PRIMARY_DATABASE"),

            PrimaryHost = Required("PGBOUNCER_PORT_HOST"),
            PrimaryPort = int.Parse(
                Required("PGBOUNCER_PORT")),

            ReplicaHost = Required("PGBOUNCER_PORT_HOST"),
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
