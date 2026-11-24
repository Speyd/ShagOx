using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Login;
public interface ILoginSessionService
{
    Task<Result<LoginResponse>> CreateSessionAsync(
        User user);
}
