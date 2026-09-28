namespace ShagOxServer.Application.DTOs.Auth.Users.Contacts.Passwords;
public sealed record ResetPasswordRequest(
    long UserId,
    string EmailOrPhoneOrUserName,
    string NewPassword
);