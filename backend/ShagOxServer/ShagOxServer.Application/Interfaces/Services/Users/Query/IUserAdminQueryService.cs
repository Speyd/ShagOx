using ShagOxServer.Application.DTOs.Users;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Users.Query;
public interface IUserAdminQueryService
{
    Task<Result<UserDto>> GetByContactAsync(
        string? email,
        string? phone);

    Task<Result<UserDto>> GetByEmailAsync(string email);

    Task<Result<UserDto>> GetByPhoneAsync(string phone);

    Task<Result<List<UserDto>>> GetByCityAsync(int cityId,
        PaginationParams pagination);

    Task<Result<List<UserDto>>> GetUsersRegisteredAfterAsync(
        DateTime date,
        PaginationParams pagination);

    Task<Result<List<UserDto>>> GetUsersActiveAfterAsync(
        DateTime date,
        PaginationParams pagination);


    Task<bool> ExistsByIdAsync(int id);
}