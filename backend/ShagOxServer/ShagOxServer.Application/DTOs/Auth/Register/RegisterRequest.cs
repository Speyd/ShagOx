namespace ShagOxServer.Application.DTOs.Auth.Register;
public sealed record RegisterRequest(
    string EmailOrPhone,
    string Password
);