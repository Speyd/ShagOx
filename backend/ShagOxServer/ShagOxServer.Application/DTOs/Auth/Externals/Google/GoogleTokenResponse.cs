using System.Text.Json.Serialization;

namespace ShagOxServer.Application.DTOs.Auth.Externals.Google;
public sealed record GoogleTokenResponse
(
    [property: JsonPropertyName("access_token")]
    string AccessToken,

    [property: JsonPropertyName("expires_in")]
    int ExpiresIn,

    [property: JsonPropertyName("refresh_token")]
    string? RefreshToken,

    [property: JsonPropertyName("scope")]
    string? Scope,

    [property: JsonPropertyName("token_type")]
    string? TokenType,

    [property: JsonPropertyName("id_token")]
    string? IdToken
);