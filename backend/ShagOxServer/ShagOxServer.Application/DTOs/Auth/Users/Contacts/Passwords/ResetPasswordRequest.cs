namespace ShagOxServer.Application.DTOs.Auth.Users.Contacts.Passwords;
public sealed record ResetPasswordRequest(
    int UserId,
    string EmailOrPhoneOrUserName,
    string NewPassword
);