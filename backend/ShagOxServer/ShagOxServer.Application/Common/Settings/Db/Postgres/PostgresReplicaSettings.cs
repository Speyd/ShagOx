namespace ShagOxServer.Application.Common.Settings.Db.Postgres;
public sealed class PostgresReplicaSettings
{
    public required string ContainerName { get; init; }
    public required int Port { get; init; }
}
