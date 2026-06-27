using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Advertisements;
using ShagOxServer.Application.DTOs.Users;

namespace ShagOxServer.Application.Interfaces.Users.Query;
public interface IUserQueryService
{
    Task<Result<UserDto>> GetByIdAsync(int id);

    Task<Result<UserDto>> GetMyProfileAsync();
}
