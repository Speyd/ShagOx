namespace ShagOxServer.Application.DTOs.Auth.Users.Contacts.Passwords;

public sealed record ConfirmResetPasswordRequest(
    int UserId,
    string Code,
    string NewPassword
);
