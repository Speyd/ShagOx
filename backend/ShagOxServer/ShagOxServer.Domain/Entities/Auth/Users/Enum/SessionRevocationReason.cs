namespace ShagOxServer.Domain.Entities.Auth.Users.Enum;
public enum SessionRevocationReason
{
    Logout = 1,
    UserRevoked = 2,
    AdminRevoked = 3,
    SecurityViolation = 4,
    ContactChanged = 5,
    PasswordChanged = 6,
    AccountBlocked = 7,
    TokenReuseDetected = 8,
    TwoFactorEnabled = 9,
    TwoFactorDisabled = 10,
}
