namespace ShagOxServer.Application.Common.Settings.Db.PgBouncer;
public sealed class PgBouncerSettings
{
    public required string User { get; init; }
    public required string Password { get; init; }
    public required string Database { get; init; }

    public required string PrimaryHost { get; init; }
    public required int PrimaryPort { get; init; }

    public required string ReplicaHost { get; init; }
    public required IReadOnlyList<PgBouncerReplicaSettings> Replicas { get; init; }
}
