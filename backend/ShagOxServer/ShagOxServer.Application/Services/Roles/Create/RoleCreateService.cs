using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Roles.Create;
using ShagOxServer.Application.Interfaces.Roles.Create;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Interfaces.Auth;

namespace ShagOxServer.Application.Services.Roles.Create;
public class RoleCreateService : IRoleCreateService
{
    private readonly IRoleRepository _repository;

    public RoleCreateService(
        IRoleRepository roleRepository)
    {
        _repository = roleRepository;
    }

    public async Task<Result<RoleCreateResponse>> CreateRoleAsync(
        RoleCreateRequest request)
    {
        var validation = await _repository.ExistsAsync(request.Name);
        if (validation)
            return Result<RoleCreateResponse>.Fail("Role ist exists");

        var role = CreateRole(request);

        await _repository.AddAsync(role);

        var response = new RoleCreateResponse(
            role.Id,
            DateTime.UtcNow
        );

        return Result<RoleCreateResponse>.Success(response);
    }

    private Role CreateRole(
       RoleCreateRequest request)
    {
        return new Role
        {
            Name = request.Name,
            Description = request.Description ?? "",
        };
    }
}
