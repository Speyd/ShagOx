namespace ShagOxServer.Application.Common.Settings.Auth;
public sealed class GoogleSettings
{
    public string ClientId { get; init; } = null!;
    public string ClientSecret { get; init; } = null!;
    public string TokenLink { get; init; } = null!;
    public string RedirectUri { get; init; } = null!;
    public string GrantType { get; init; } = null!;
}