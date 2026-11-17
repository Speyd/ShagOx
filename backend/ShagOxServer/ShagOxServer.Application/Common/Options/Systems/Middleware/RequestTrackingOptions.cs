namespace ShagOxServer.Application.Common.Options.Systems.Middleware;
public sealed class RequestTrackingOptions
{
    public int MaxTokens { get; init; }
    public int RequestsPerToken { get; init; }
    public int BanLevelCount { get; init; }
    public TimeSpan BanDuration { get; init; }
    public TimeSpan CacheDuration { get; init; }
}
