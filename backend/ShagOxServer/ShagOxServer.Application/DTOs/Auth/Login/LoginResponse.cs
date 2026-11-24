namespace ShagOxServer.Application.DTOs.Auth.Login;
public sealed record LoginResponse
(
    string? AccessToken = null,
    string? RefreshToken = null
);
