using ShagOxServer.Application.DTOs.Roles;
using ShagOxServer.Application.DTOs.Users;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Users.Query;
using ShagOxServer.Application.Services.Roles.Mapping;
using ShagOxServer.Application.Services.Users.Mapping;
using ShagOxServer.Domain.Filters.Users;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Users.Query;
public class UserQueryService : IUserQueryService
{
    private readonly IUserQueryRepository _repository;
    private readonly IRoleQueryRepository _roleRepository;

    private readonly IUserContext _context;

    public UserQueryService(
        IUserQueryRepository userRepository,
        IRoleQueryRepository roleRepository,
        IUserContext userContext)
    {
        _repository = userRepository;
        _roleRepository = roleRepository;
        _context = userContext;
    }

    public async Task<Result<UserDto>> GetByIdAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);

        return user.ToResult(UserMapper.ToDto);
    }

    public async Task<Result<UserDto>> GetMyProfileAsync()
    {
        var user = await _repository.GetByIdAsync(_context.UserId);

        return user.ToResult(UserMapper.ToDto);
    }

    public async Task<Result<List<RoleDto>>> GetMyRoleAsync(
        PaginationParams pagination)
    {
        var roles = await _roleRepository.GetByUserIdAsync(_context.UserId, pagination);

        return roles.ToResultList(RoleMapper.ToDto);
    }

    public async Task<Result<List<UserDto>>> Search(
       UserSearchFilter filter,
	   PaginationParams pagination)
    {
        var users = await _repository.Search(
            filter, pagination);

        return users.ToResultList(UserMapper.ToDto);
    }
}