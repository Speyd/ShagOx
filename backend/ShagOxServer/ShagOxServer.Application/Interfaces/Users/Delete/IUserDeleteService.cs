using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Users.Delete;

namespace ShagOxServer.Application.Interfaces.Users.Delete;
public interface IUserDeleteService
{
    Task<Result<UserDeleteResponse>> DeleteUserAsync(
        int id);
}
