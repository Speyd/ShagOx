using ShagOxServer.Application.DTOs.Auth.Register;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Auth;

public interface IRegisterService
{
    Task<Result<RegisterResponse>> RegisterAsync(
       RegisterRequest request);
}
