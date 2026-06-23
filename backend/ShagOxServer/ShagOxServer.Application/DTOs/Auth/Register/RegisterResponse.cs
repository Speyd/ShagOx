namespace ShagOxServer.Application.DTOs.Auth.Register;

public sealed record RegisterResponse(
    int Id,
    string EmailOrPhone,
    string UserName
);