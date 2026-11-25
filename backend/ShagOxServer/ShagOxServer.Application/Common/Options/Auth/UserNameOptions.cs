namespace ShagOxServer.Application.Common.Options.Auth;
public sealed class UserNameOptions
{
    public const string SectionName = "UserName";

    public int MaxAttempts { get; set; }
}
