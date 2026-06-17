
namespace ShagOxServer.Application.DTOs.Auth;

public sealed record RegisterResponse(
    int Id,
    string EmailOrPhone,
    string UserName
);