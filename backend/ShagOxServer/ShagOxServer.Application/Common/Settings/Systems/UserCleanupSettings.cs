namespace ShagOxServer.Application.Common.Settings.Systems;
public sealed class UserCleanupSettings
{
    public TimeSpan CleanupInterval { get; init; }
    public TimeSpan UserLifetime { get; init; }
}
