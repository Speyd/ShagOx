using ShagOxServer.Application.DTOs.Roles;
using ShagOxServer.Application.DTOs.Users;
using ShagOxServer.Application.Interfaces.Repositories.Auth;
using ShagOxServer.Application.Interfaces.Services.UserRoles;
using ShagOxServer.Application.Services.Roles.Mapping;
using ShagOxServer.Application.Services.Users.Mapping;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.UserRoles;
public class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository _repository;

    public UserRoleService(
        IUserRoleRepository userRoleRepository)
    {
        _repository = userRoleRepository;
    }

    public async Task<Result<List<RoleDto>>> GetRolesByUserIdAsync(
        int userId,
		PaginationParams pagination)
    {
        var roles = await _repository.GetRolesByUserIdAsync(userId, pagination);

        return roles.ToResultList(RoleMapper.ToDto);
    }

    public async Task<Result<List<UserDto>>> GetUsersByRoleIdAsync(
        int roleId,
		PaginationParams pagination)
    {
        var users = await _repository.GetUsersByRoleIdAsync(roleId, pagination);

        return users.ToResultList(UserMapper.ToDto);
    }
}