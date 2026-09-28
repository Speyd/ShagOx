using ShagOxServer.Application.DTOs.Auth.Roles;
using ShagOxServer.Application.DTOs.Auth.UserRoles;
using ShagOxServer.Application.DTOs.Auth.Users.Core;
using ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles;
using ShagOxServer.Application.Interfaces.Services.Auth.UserRoles.Query;
using ShagOxServer.Application.Services.Auth.Roles.Mapping;
using ShagOxServer.Application.Services.Auth.UserRoles.Mapping;
using ShagOxServer.Application.Services.Auth.Users.Core.Mapping;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.UserRoles;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Auth.UserRoles.Query;
public class UserRoleQueryService 
    : BaseQueryService<
        UserRoleDto,
        UserRole,
        UserRoleSearchFilter
        >,
    IUserRoleQueryService
{
    private readonly IUserRoleQueryRepository _userRoleQueryRepository;


    public UserRoleQueryService(
        IUserRoleQueryRepository userRoleQueryRepository
    )
        : base(userRoleQueryRepository)
    {
        _userRoleQueryRepository = userRoleQueryRepository;
    }


    protected override async Task<UserRoleDto> ApplyMapperAsync(
        UserRole entity)
    {
        return UserRoleMapper.ToDto(entity);
    }

    public async Task<Result<PagedResult<RoleDto>>> GetRolesByUserIdAsync(
        long userId,
		PaginationParams pagination)
    {
        var roles = await _userRoleQueryRepository
            .GetRolesByUserIdAsync(userId, pagination);

        return roles.ToResultPaged(RoleMapper.ToDto);
    }

    public async Task<Result<PagedResult<UserDto>>> GetUsersByRoleIdAsync(
        long roleId,
		PaginationParams pagination)
    {
        var users = await _userRoleQueryRepository
            .GetUsersByRoleIdAsync(roleId, pagination);

        return users.ToResultPaged(UserMapper.ToDto);
    }
}