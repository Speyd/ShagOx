namespace ShagOxServer.Application.Common.Options.Systems.BackgroundServices.User;
public sealed class UserBanOptions
{
    public const string SectionName = "UserBan";

    public TimeSpan WaitTimer { get; init; }
}
