namespace ShagOxServer.Application.Common.Options.Systems.Middleware;
public sealed class UpdateLastSeenOptions
{
    public const string SectionName = "UpdateLastSeen";

    public TimeSpan UpdateLastSeen { get; init; }
}
