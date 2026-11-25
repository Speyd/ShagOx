namespace ShagOxServer.Application.Common.Options.Db.PgBouncer;
public sealed class PgBouncerOptions
{
    public const string SectionName = "PgBouncer";

    public required string User { get; init; }
    public required string Password { get; init; }
    public required string Database { get; init; }

    public required string PrimaryHost { get; init; }
    public required int PrimaryPort { get; init; }

    public required string ReplicaHost { get; init; }
    public required IReadOnlyList<PgBouncerReplicaOptions> Replicas { get; init; }
}
