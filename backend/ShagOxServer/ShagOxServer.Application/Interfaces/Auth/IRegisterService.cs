using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Auth.Register;

namespace ShagOxServer.Application.Interfaces.Auth;

public interface IRegisterService
{
    Task<Result<RegisterResponse>> RegisterAsync(
       RegisterRequest request);
}
