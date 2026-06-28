using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Users.Update;

namespace ShagOxServer.Application.Interfaces.Users.Update;
public interface IUserUpdateService
{
    Task<Result<UserUpdateResponse>> UpdateUserAsync(
        int userId,
        UserUpdateRequest request);
}
