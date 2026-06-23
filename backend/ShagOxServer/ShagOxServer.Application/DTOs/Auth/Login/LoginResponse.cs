namespace ShagOxServer.Application.DTOs.Auth.Login;
public sealed record LoginResponse(
    bool Success,
    string Token,
    string Message
);