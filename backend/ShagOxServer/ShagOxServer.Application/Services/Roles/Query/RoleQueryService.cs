using ShagOxServer.SharedKernel.Results;
using ShagOxServer.SharedKernel.Results.Extensions;
using ShagOxServer.Application.DTOs.Roles;
using ShagOxServer.Application.Interfaces.Roles.Query;
using ShagOxServer.Application.Services.Roles.Mapping;
using ShagOxServer.Infrastructure.Interfaces.Auth.Roles;

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

    public async Task<Result<RoleDto>> GetByNameAsync(string name)
    {
        var role = await _repository.GetByNameAsync(name);

        return role.ToResult(RoleMapper.ToDto);
    }

    public async Task<Result<List<RoleDto>>> SearchByName(
       string name,
       int page,
       int pageSize)
    {
        var roles = await _repository.SearchByName(name, page, pageSize);

        return roles.ToResultList(RoleMapper.ToDto);
    }
}
