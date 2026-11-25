namespace ShagOxServer.Application.Common.Options.Db.Postgres;
public sealed class PostgresOptions
{
    public const string SectionName = "Postgres";

    public required string User { get; init; }
    public required string Password { get; init; }
    public required string Database { get; init; }

    public required string PrimaryHost { get; init; }
    public required int PrimaryPort { get; init; }

    public required string ReplicaHost { get; init; }
    public required IReadOnlyList<PostgresReplicaOptions> Replicas { get; init; }
}
