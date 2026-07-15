using ShagOxServer.Application.DTOs.Roles;
using ShagOxServer.Application.DTOs.Users;
using ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles;
using ShagOxServer.Application.Interfaces.Services.UserRoles.Query;
using ShagOxServer.Application.Services.Roles.Mapping;
using ShagOxServer.Application.Services.Users.Mapping;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.UserRoles.Query;
public class UserRoleQueryService : IUserRoleQueryService
{
    private readonly IUserRoleQueryRepository _queryRepository;

    public UserRoleQueryService(
        IUserRoleQueryRepository queryRepository)
    {
        _queryRepository = queryRepository;
    }

    public async Task<Result<List<RoleDto>>> GetRolesByUserIdAsync(
        int userId,
		PaginationParams pagination)
    {
        var roles = await _queryRepository.GetRolesByUserIdAsync(userId, pagination);

        return roles.ToResultList(RoleMapper.ToDto);
    }

    public async Task<Result<List<UserDto>>> GetUsersByRoleIdAsync(
        int roleId,
		PaginationParams pagination)
    {
        var users = await _queryRepository.GetUsersByRoleIdAsync(roleId, pagination);

        return users.ToResultList(UserMapper.ToDto);
    }
}