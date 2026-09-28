using ShagOxServer.Application.DTOs.Auth.Roles;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Application.Interfaces.Services.Auth.Roles.Query;
using ShagOxServer.Application.Services.Auth.Roles.Mapping;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.Roles;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Auth.Roles.Query;
public class RoleQueryService 
    : BaseQueryService<
        RoleDto,
        Role,
        RoleSearchFilter
        >,
    IRoleQueryService
{
    private readonly IRoleQueryRepository _roleQueryRepository;


    public RoleQueryService(
        IRoleQueryRepository roleQueryRepository
    )
        : base(roleQueryRepository)
    {
        _roleQueryRepository = roleQueryRepository;
    }


    public override async Task<RoleDto> ApplyMapperAsync(
        Role entity)
    {
        return RoleMapper.ToDto(entity);
    }

    public async Task<Result<PagedResult<RoleDto>>> GetByUserAsync(
       long userId,
       PaginationParams pagination)
    {
        var roles = await _roleQueryRepository
            .GetByUserAsync(userId, pagination);

        return await roles.ToResultPagedAsync(ApplyMapperAsync);
    }

    public async Task<Result<RoleDto>> GetByNameAsync(
        string name)
    {
        var role = await _roleQueryRepository
            .GetByNameAsync(name);

        return await role.ToResultAsync(ApplyMapperAsync);
    }
}