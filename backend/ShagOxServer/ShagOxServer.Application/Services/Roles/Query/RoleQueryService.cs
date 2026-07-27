using ShagOxServer.Application.DTOs.Roles;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Application.Interfaces.Services.Roles.Query;
using ShagOxServer.Application.Services.Roles.Mapping;
using ShagOxServer.Domain.Filters.Roles;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Roles.Query;
public class RoleQueryService : IRoleQueryService
{
    private readonly IRoleQueryRepository _roleQueryRepository;


    public RoleQueryService(
        IRoleQueryRepository roleQueryRepository)
    {
        _roleQueryRepository = roleQueryRepository;
    }


    public async Task<Result<RoleDto>> GetByIdAsync(int id)
    {
        var role = await _roleQueryRepository
            .GetByIdAsync(id);

        return role.ToResult(RoleMapper.ToDto);
    }

    public async Task<Result<PagedResult<RoleDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var roles = await _roleQueryRepository
            .GetPagedAsync(pagination);

        return roles.ToResultPaged(RoleMapper.ToDto);
    }

    public async Task<Result<PagedResult<RoleDto>>> GetByUserAsync(
       int userId,
       PaginationParams pagination)
    {
        var roles = await _roleQueryRepository
            .GetByUserAsync(userId, pagination);

        return roles.ToResultPaged(RoleMapper.ToDto);
    }

    public async Task<Result<RoleDto>> GetByNameAsync(string name)
    {
        var role = await _roleQueryRepository
            .GetByNameAsync(name);

        return role.ToResult(RoleMapper.ToDto);
    }

    public async Task<Result<PagedResult<RoleDto>>> Search(
       RoleSearchFilter filter,
	   PaginationParams pagination)
    {
        var roles = await _roleQueryRepository
            .Search(filter, pagination);

        return roles.ToResultPaged(RoleMapper.ToDto);
    }
}