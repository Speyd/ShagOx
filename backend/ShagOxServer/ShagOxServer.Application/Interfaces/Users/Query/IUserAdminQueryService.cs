using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Users;

namespace ShagOxServer.Application.Interfaces.Users.Query;
public interface IUserAdminQueryService
{
    Task<Result<UserDto>> GetByContactAsync(string? email, string? phone);

    Task<Result<UserDto>> GetByEmailAsync(string email);

    Task<Result<UserDto>> GetByPhoneAsync(string phone);

    Task<Result<List<UserDto>>> GetByCityAsync(int cityId);

    Task<Result<List<UserDto>>> GetUsersRegisteredAfterAsync(DateTime date);

    Task<Result<List<UserDto>>> GetUsersActiveAfterAsync(DateTime date);


    Task<bool> ExistsAsync(int id);

    Task<bool> ExistsAdvertisementAsync(int userId, int AdvertId);
}
