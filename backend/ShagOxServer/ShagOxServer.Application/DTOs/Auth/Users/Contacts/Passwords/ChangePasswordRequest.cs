namespace ShagOxServer.Application.DTOs.Auth.Users.Contacts.Passwords;
public sealed record ChangePasswordRequest(
    string NewPassword,
    string OldPassword
);