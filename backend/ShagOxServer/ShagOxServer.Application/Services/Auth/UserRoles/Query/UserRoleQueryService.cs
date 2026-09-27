using ShagOxServer.Application.DTOs.Auth.Roles;
using ShagOxServer.Application.DTOs.Auth.UserRoles;
using ShagOxServer.Application.DTOs.Auth.Users.Core;
using ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles;
using ShagOxServer.Application.Interfaces.Services.Auth.UserRoles.Query;
using ShagOxServer.Application.Services.Auth.Roles.Mapping;
using ShagOxServer.Application.Services.Auth.UserRoles.Mapping;
using ShagOxServer.Application.Services.Auth.Users.Core.Mapping;
using ShagOxServer.Domain.Filters.Auth.UserRoles;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Auth.UserRoles.Query;
public class UserRoleQueryService 
    : IUserRoleQueryService
{
    private readonly IUserRoleQueryRepository _queryRepository;


    public UserRoleQueryService(
        IUserRoleQueryRepository queryRepository)
    {
        _queryRepository = queryRepository;
    }


    public async Task<Result<UserRoleDto>> GetByIdAsync(
        long id)
    {
        var advert = await _queryRepository
            .GetByIdAsync(id);

        return advert.ToResult(UserRoleMapper.ToDto);
    }

    public async Task<Result<PagedResult<UserRoleDto>>> GetPagedAsync(
       PaginationParams pagination)
    {
        var adverts = await _queryRepository
            .GetPagedAsync(pagination);

        return adverts.ToResultPaged(UserRoleMapper.ToDto);
    }

    public async Task<Result<PagedResult<RoleDto>>> GetRolesByUserIdAsync(
        long userId,
		PaginationParams pagination)
    {
        var roles = await _queryRepository
            .GetRolesByUserIdAsync(userId, pagination);

        return roles.ToResultPaged(RoleMapper.ToDto);
    }

    public async Task<Result<PagedResult<UserDto>>> GetUsersByRoleIdAsync(
        long roleId,
		PaginationParams pagination)
    {
        var users = await _queryRepository
            .GetUsersByRoleIdAsync(roleId, pagination);

        return users.ToResultPaged(UserMapper.ToDto);
    }

    public async Task<Result<PagedResult<UserRoleDto>>> Search(
        UserRoleSearchFilter filter,
        PaginationParams pagination)
    {
        var users = await _queryRepository
            .SearchAsync(filter, pagination);

        return users.ToResultPaged(UserRoleMapper.ToDto);
    }
}