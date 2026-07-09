using ShagOxServer.Application.DTOs.Users.Update;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Users.Update;
public interface IUserUpdateService
{
    Task<Result<UserUpdateResponse>> UpdateUserAsync(
        int userId,
        UserUpdateRequest request);
}
