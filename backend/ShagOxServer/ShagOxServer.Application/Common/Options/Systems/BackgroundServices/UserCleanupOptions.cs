namespace ShagOxServer.Application.Common.Options.Systems.BackgroundServices;
public sealed class UserCleanupOptions
{
    public TimeSpan CleanupInterval { get; init; }
    public TimeSpan UserLifetime { get; init; }
}
