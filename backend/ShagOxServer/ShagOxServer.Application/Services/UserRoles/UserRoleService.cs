using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.DTOs.Roles;
using ShagOxServer.Application.DTOs.Users;
using ShagOxServer.Application.Interfaces.UserRoles;
using ShagOxServer.Application.Services.Roles.Mapping;
using ShagOxServer.Application.Services.Users.Mapping;
using ShagOxServer.Infrastructure.Interfaces.Auth;

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
        int userId)
    {
        var roles = await _repository.GetRolesByUserIdAsync(userId);

        return roles.ToResultList(RoleMapper.ToDto);
    }

    public async Task<Result<List<UserDto>>> GetUsersByRoleIdAsync(
        int roleId,
        int page = 1,
        int pageSize = 20)
    {
        var users = await _repository.GetUsersByRoleIdAsync(roleId, page, pageSize);

        return users.ToResultList(UserMapper.ToDto);
    }
}
