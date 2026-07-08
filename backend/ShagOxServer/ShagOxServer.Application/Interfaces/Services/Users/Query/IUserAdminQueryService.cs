using ShagOxServer.Application.DTOs.Users;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Users.Query;
public interface IUserAdminQueryService
{
    Task<Result<UserDto>> GetByContactAsync(string? email, string? phone);

    Task<Result<UserDto>> GetByEmailAsync(string email);

    Task<Result<UserDto>> GetByPhoneAsync(string phone);

    Task<Result<List<UserDto>>> GetByCityAsync(int cityId);

    Task<Result<List<UserDto>>> GetUsersRegisteredAfterAsync(DateTime date);

    Task<Result<List<UserDto>>> GetUsersActiveAfterAsync(DateTime date);


    Task<bool> ExistsAsync(int id);

    Task<bool> IsAdvertisementOwnerAsync(int userId, int AdvertId);
}
