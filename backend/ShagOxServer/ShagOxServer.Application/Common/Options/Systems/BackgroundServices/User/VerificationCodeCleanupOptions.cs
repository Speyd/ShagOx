namespace ShagOxServer.Application.Common.Options.Systems.BackgroundServices;
public sealed class VerificationCodeCleanupOptions
{
    public const string SectionName = "VerificationCodeCleanup";

    public TimeSpan CleanupInterval { get; init; }
}
