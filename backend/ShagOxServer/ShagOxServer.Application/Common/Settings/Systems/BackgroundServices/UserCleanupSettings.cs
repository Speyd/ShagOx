namespace ShagOxServer.Application.Common.Settings.Systems.BackgroundServices;
public sealed class UserCleanupSettings
{
    public TimeSpan CleanupInterval { get; init; }
    public TimeSpan UserLifetime { get; init; }
}
