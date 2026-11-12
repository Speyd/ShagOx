namespace ShagOxServer.Application.Common.Options.Db.PgBouncer;
public sealed class PgBouncerReplicaSettings
{
    public required string Database { get; init; }
    public string ContainerName { get; init; } = "";
    public required int Port { get; init; }
}
