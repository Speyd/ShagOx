using ShagOxServer.Application.DTOs.Auth.Roles;
using ShagOxServer.Application.DTOs.Auth.UserRoles;
using ShagOxServer.Application.DTOs.Auth.Users;
using ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles;
using ShagOxServer.Application.Interfaces.Services.Auth.UserRoles.Query;
using ShagOxServer.Application.Services.Auth.Roles.Mapping;
using ShagOxServer.Application.Services.Auth.UserRoles.Mapping;
using ShagOxServer.Application.Services.Auth.Users.Mapping;
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
        int id)
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

    public async Task<Result<List<RoleDto>>> GetRolesByUserIdAsync(
        int userId,
		PaginationParams pagination)
    {
        var roles = await _queryRepository
            .GetRolesByUserIdAsync(userId, pagination);

        return roles.ToResultList(RoleMapper.ToDto);
    }

    public async Task<Result<List<UserDto>>> GetUsersByRoleIdAsync(
        int roleId,
		PaginationParams pagination)
    {
        var users = await _queryRepository
            .GetUsersByRoleIdAsync(roleId, pagination);

        return users.ToResultList(UserMapper.ToDto);
    }
}