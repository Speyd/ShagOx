namespace ShagOxServer.Application.DTOs.Auth.Login;
public sealed record LoginRequest
(
    string EmailOrPhoneOrUserName,
    string Password
);