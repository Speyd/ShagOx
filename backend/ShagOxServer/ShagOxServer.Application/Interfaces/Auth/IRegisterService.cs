using ShagOxServer.Application.DTOs.Auth.Register;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Interfaces.Auth;

public interface IRegisterService
{
    Task<RegisterResponse> RegisterAsync(
       RegisterRequest request);
}
