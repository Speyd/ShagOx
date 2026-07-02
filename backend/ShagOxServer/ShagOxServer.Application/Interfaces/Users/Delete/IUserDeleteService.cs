using ShagOxServer.Application.DTOs.Users.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Users.Delete;
public interface IUserDeleteService
{
    Task<Result<UserDeleteResponse>> DeleteUserAsync(
        int id);
}
