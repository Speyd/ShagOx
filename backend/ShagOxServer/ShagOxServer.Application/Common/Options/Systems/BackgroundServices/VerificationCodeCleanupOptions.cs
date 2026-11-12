namespace ShagOxServer.Application.Common.Options.Systems.BackgroundServices;
public sealed class VerificationCodeCleanupOptions
{
    public TimeSpan CleanupInterval { get; init; }
}
