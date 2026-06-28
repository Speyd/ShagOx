namespace ShagOxServer.Application.DTOs.Auth.Login;

public sealed record LoginResponse(
    string? Token = null
);