namespace ShagOxServer.Application.Common.Options.Auth;
public sealed class GoogleOptions
{
    public const string SectionName = "Google";

    public string ClientId { get; init; } = null!;
    public string ClientSecret { get; init; } = null!;
    public string TokenLink { get; init; } = null!;
    public string RedirectUri { get; init; } = null!;
    public string GrantType { get; init; } = null!;
}
