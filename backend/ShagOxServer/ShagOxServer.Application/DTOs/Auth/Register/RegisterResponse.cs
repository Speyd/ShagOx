using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.DTOs.Auth.Register;

public sealed record RegisterResponse(
    int Id,
    string EmailOrPhone,
    string UserName
)
{
    public RegisterResponse(User user)
        : this(
            user.Id,
            user.Email ?? user.Phone ?? "",
            user.Name ?? ""
        )
    {}
}