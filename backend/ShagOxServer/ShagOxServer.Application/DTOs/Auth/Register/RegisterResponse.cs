using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.DTOs.Auth.Register;

public sealed record RegisterResponse(
    long Id,
    string EmailOrPhone,
    string UserName,
    bool EmailVerificationRequired,
    bool PhoneVerificationRequired
)
{
    public RegisterResponse(User user)
       : this(
           user.Id,
           user.Email ?? user.Phone ?? "",
           user.UserName,
           !string.IsNullOrWhiteSpace(user.Email) && !user.EmailConfirmed,
           !string.IsNullOrWhiteSpace(user.Phone) && !user.PhoneConfirmed
       )
    {
    }
}