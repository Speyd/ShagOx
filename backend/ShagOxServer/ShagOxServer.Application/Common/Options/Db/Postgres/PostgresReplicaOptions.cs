namespace ShagOxServer.Application.Common.Options.Db.Postgres;
public sealed class PostgresReplicaOptions
{
    public required string ContainerName { get; init; }
    public required int Port { get; init; }
}
