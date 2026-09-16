namespace ShagOxServer.Application.Common.Settings;
public sealed class VerificationCodeSettings
{
    public int MinGenValue { get; init; } = 100000;
    public int MaxGenValue { get; init; } = 1_000_000;
    public int MaxAttempts { get; init; } = 3;
    public int ExpirationMinutes { get; init; } = 10;
}