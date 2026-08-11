using ShagOxServer.Application.DTOs.Auth.Roles;
using ShagOxServer.Application.DTOs.Auth.Users;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Services.Auth.Roles.Mapping;
using ShagOxServer.Application.Services.Auth.Users.Mapping;
using ShagOxServer.Domain.Filters.Users;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Auth.Users.Query;
public class UserQueryService 
    : IUserQueryService
{
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IRoleQueryRepository _roleQueryRepository;

    private readonly IUserContext _context;


    public UserQueryService(
        IUserQueryRepository userQueryRepository,
        IRoleQueryRepository roleQueryRepository,
        IUserContext userContext)
    {
        _userQueryRepository = userQueryRepository;
        _roleQueryRepository = roleQueryRepository;
        _context = userContext;
    }


    public async Task<Result<UserDto>> GetByIdAsync(
        int id)
    {
        var user = await _userQueryRepository
            .GetByIdAsync(id);

        return user.ToResult(UserMapper.ToDto);
    }

    public async Task<Result<UserDto>> GetMyProfileAsync()
    {
        var user = await _userQueryRepository
            .GetByIdAsync(_context.UserId);

        return user.ToResult(UserMapper.ToDto);
    }

    public async Task<Result<PagedResult<RoleDto>>> GetMyRoleAsync(
        PaginationParams pagination)
    {
        var roles = await _roleQueryRepository
            .GetByUserAsync(_context.UserId, pagination);

        return roles.ToResultPaged(RoleMapper.ToDto);
    }

    public async Task<Result<PagedResult<UserDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var users = await _userQueryRepository
            .GetPagedAsync(pagination);

        return users.ToResultPaged(UserMapper.ToDto);
    }

    public async Task<Result<PagedResult<UserDto>>> Search(
       UserSearchFilter filter,
	   PaginationParams pagination)
    {
        var users = await _userQueryRepository
            .Search(
            filter, pagination);

        return users.ToResultPaged(UserMapper.ToDto);
    }
}