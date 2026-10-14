namespace ShagOxServer.Application.Common.Settings.Systems.BackgroundServices;
public sealed class VerificationCodeCleanupSettings
{
    public TimeSpan CleanupInterval { get; init; }
}
