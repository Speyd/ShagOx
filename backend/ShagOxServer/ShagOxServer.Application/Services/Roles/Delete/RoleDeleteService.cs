using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Roles.Delete;
using ShagOxServer.Application.Interfaces.Roles.Delete;
using ShagOxServer.Infrastructure.Interfaces.Auth.Roles;

namespace ShagOxServer.Application.Services.Roles.Delete;
public class RoleDeleteService : IRoleDeleteService
{
    private readonly IRoleRepository _repository;

    public RoleDeleteService(
        IRoleRepository roleRepository)
    {
        _repository = roleRepository;
    }

    public async Task<Result<RoleDeleteResponse>> DeleteRoleAsync(int id)
    {
        var role = await _repository.GetByIdAsync(id);
        if (role is null)
            return Result<RoleDeleteResponse>.NotFound("Role");

        await _repository.DeleteAsync(role);
        return Result<RoleDeleteResponse>.Success(
           new RoleDeleteResponse(
               role.Id,
               DateTime.UtcNow
           )
       );
    }
}
