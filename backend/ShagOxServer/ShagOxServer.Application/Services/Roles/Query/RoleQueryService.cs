using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.DTOs.Roles;
using ShagOxServer.Application.Interfaces.Roles.Query;
using ShagOxServer.Application.Services.Roles.Mapping;
using ShagOxServer.Infrastructure.Interfaces.Auth.Roles;

namespace ShagOxServer.Application.Services.Roles.Query;
public class RoleQueryService : IRoleQueryService
{
    private readonly IRoleRepository _repository;

    public RoleQueryService(
        IRoleRepository roleRepository)
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
}
