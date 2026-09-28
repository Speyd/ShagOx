namespace ShagOxServer.Application.DTOs.Auth.Users.Contacts.Passwords;
public sealed record ConfirmResetPasswordRequest(
    long UserId,
    string Code,
    string NewPassword
);
