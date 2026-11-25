namespace ShagOxServer.Application.Common.Options.Systems.BackgroundServices.User;
public sealed class UserDeviceBanOptions
{
    public const string SectionName = "UserDeviceBan";

    public TimeSpan WaitTimer { get; init; }
}
