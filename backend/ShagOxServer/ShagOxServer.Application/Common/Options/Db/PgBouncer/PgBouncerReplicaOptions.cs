namespace ShagOxServer.Application.Common.Options.Db.PgBouncer;
public sealed class PgBouncerReplicaOptions
{
    public const string SectionName = "PgBouncerReplica";

    public required string Database { get; init; }
    public string ContainerName { get; init; } = "";
    public required int Port { get; init; }
}
