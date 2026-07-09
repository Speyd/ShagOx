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
    private readonly IRoleQueryRepository _repository;


    public RoleQueryService(
        IRoleQueryRepository roleRepository)
    {
        _repository = roleRepository;
    }

    public async Task<Result<RoleDto>> GetByIdAsync(int id)
    {
        var role = await _repository.GetByIdAsync(id);

        return role.ToResult(RoleMapper.ToDto);
    }

    public async Task<Result<List<RoleDto>>> GetByUserIdAsync(
       int userId,
       PaginationParams pagination)
    {
        var roles = await _repository.GetByUserIdAsync(userId, pagination);

        return roles.ToResultList(RoleMapper.ToDto);
    }

    public async Task<Result<RoleDto>> GetByNameAsync(string name)
    {
        var role = await _repository.GetByNameAsync(name);

        return role.ToResult(RoleMapper.ToDto);
    }

    public async Task<Result<List<RoleDto>>> Search(
       RoleSearchFilter filter,
	   PaginationParams pagination)
    {
        var roles = await _repository.Search(filter, pagination);

        return roles.ToResultList(RoleMapper.ToDto);
    }
}
