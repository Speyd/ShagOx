using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Users;

namespace ShagOxServer.Application.Interfaces.Users.Query;
public interface IUserQueryService
{
    Task<Result<UserDto>> GetByIdAsync(int id);

    Task<Result<UserDto>> GetMyProfileAsync();

    Task<Result<List<UserDto>>> SearchByFullName(
        string fullName,
        int page,
        int pageSize);

    Task<Result<List<UserDto>>> SearchByEmail(
        string email,
        int page,
        int pageSize);

    Task<Result<List<UserDto>>> SearchByPhone(
        string phone,
        int page,
        int pageSize);
}