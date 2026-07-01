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
    private readonly IRoleQueryRepository _queryRepository;


    public RoleQueryService(
        IRoleRepository roleRepository,
        IRoleQueryRepository queryRepository)
    {
        _repository = roleRepository;
        _queryRepository = queryRepository;
    }

    public async Task<Result<RoleDto>> GetByIdAsync(int id)
    {
        var role = await _repository.GetByIdAsync(id);

        return role.ToResult(RoleMapper.ToDto);
    }

    public async Task<Result<RoleDto>> GetByNameAsync(string name)
    {
        var role = await _queryRepository.GetByNameAsync(name);

        return role.ToResult(RoleMapper.ToDto);
    }

    public async Task<Result<List<RoleDto>>> SearchByName(
       string name,
       int page,
       int pageSize)
    {
        var roles = await _queryRepository.SearchByName(name, page, pageSize);

        return roles.ToResultList(RoleMapper.ToDto);
    }
}
