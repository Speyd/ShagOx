namespace ShagOxServer.Application.Common.Settings;
public sealed class BackgroundServiceSettings
{
    public TimeSpan PendingUserCleanupInterval { get; init; }
    public TimeSpan PendingUserLifetime { get; init; }

    public TimeSpan VerificationCodeCleanupInterval { get; init; }
}