namespace ShagOxServer.Application.DTOs.Auth.Externals.Google;
public sealed record GoogleTokenResponse
(
    string AccessToken,

    int ExpiresIn,

    string? RefreshToken,

    string? Scope,

    string? TokenType,

    string? IdToken
);