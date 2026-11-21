namespace ShagOxServer.Application.Common.Options.Db;
public sealed class DatabaseHealthCheckOptions
{
    public TimeSpan CheckInterval { get; init; }
    public int CommandTimeoutSeconds { get; init; }
    public TimeSpan CheckTimeout { get; init; }
}
