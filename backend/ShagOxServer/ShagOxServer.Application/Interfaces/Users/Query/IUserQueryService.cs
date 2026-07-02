using ShagOxServer.Application.DTOs.Roles;
using ShagOxServer.Application.DTOs.Users;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Users.Query;
public interface IUserQueryService
{
    Task<Result<UserDto>> GetByIdAsync(int id);

    Task<Result<UserDto>> GetMyProfileAsync();

    Task<Result<List<RoleDto>>> GetMyRoleAsync(
        PaginationParams pagination);

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