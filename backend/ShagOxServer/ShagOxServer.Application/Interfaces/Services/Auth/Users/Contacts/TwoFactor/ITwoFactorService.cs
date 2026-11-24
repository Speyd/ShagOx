using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts.TwoFactor;
public interface ITwoFactorService
{
    Task<Result<bool>> Enable();
}
