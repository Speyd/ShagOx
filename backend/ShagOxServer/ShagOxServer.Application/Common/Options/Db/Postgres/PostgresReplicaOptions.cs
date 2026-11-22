namespace ShagOxServer.Application.Common.Options.Db.Postgres;
public sealed class PostgresReplicaOptions
{
    public const string SectionName = "PostgresReplica";

    public required string ContainerName { get; init; }
    public required int Port { get; init; }
}
