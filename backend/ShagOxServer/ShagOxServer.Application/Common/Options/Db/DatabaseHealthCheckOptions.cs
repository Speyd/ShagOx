namespace ShagOxServer.Application.Common.Options.Db;
public sealed class DatabaseHealthCheckOptions
{
    public const string SectionName = "DatabaseHealthCheck";

    public TimeSpan CheckInterval { get; init; }
    public int CommandTimeoutSeconds { get; init; }
    public TimeSpan CheckTimeout { get; init; }
}
