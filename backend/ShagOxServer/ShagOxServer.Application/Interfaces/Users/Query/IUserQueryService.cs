using ShagOxServer.Application.DTOs.Users;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Paginations;

namespace ShagOxServer.Application.Interfaces.Users.Query;
public interface IUserQueryService
{
    Task<Result<UserDto>> GetByIdAsync(int id);

    Task<Result<UserDto>> GetMyProfileAsync();

    Task<Result<List<UserDto>>> SearchByFullName(
        string fullName,
        PaginationParams pagination);

    Task<Result<List<UserDto>>> SearchByEmail(
        string email,
        PaginationParams pagination);

    Task<Result<List<UserDto>>> SearchByPhone(
        string phone,
        PaginationParams pagination);
}