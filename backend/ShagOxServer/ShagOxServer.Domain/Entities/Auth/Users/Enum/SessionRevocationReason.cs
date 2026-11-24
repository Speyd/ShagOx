namespace ShagOxServer.Domain.Entities.Auth.Users.Enum;
public enum SessionRevocationReason
{
    Logout = 1,
    UserRevoked = 2,
    AdminRevoked = 3,
    SecurityViolation = 4,
    PasswordChanged = 5,
    AccountBlocked = 6,
    TokenReuseDetected = 7
}
