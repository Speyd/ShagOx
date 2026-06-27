using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Users.Create;

namespace ShagOxServer.Application.Interfaces.Users.Create;
public interface IUserUpdateService
{
    Task<Result<UserUpdateResponse>> UpdateUserAsync(
       UserUpdateRequest request);
}
