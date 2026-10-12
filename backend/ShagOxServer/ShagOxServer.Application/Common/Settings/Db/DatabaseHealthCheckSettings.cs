namespace ShagOxServer.Application.Common.Settings.Db;
public sealed class DatabaseHealthCheckSettings
{
    public TimeSpan CheckInterval { get; init; }
    public int CommandTimeoutSeconds { get; init; }
    public TimeSpan CheckTimeout { get; init; }
}
