using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Roles.Validator;
public class RoleValidator
{
    private readonly IRoleRepository _roleRepository;
    private readonly IRoleExistsRepository _roleExistsRepository;


    public RoleValidator(
        IRoleRepository roleRepository,
        IRoleExistsRepository roleExistsRepository)
    {
        _roleRepository = roleRepository;
        _roleExistsRepository = roleExistsRepository;
    }


    public async Task<Result<Role>> GetRoleValidator(
        int roleId)
    {
        var role = await _roleRepository.GetByIdAsync(roleId);
        if (role is null)
            return Result<Role>.NotFound("Role");

        return Result<Role>.Success(role);
    }

    public async Task<Result<bool>> ExistsRoleValidator(
       int roleId)
    {
        var role = await _roleExistsRepository.ExistsAsync(roleId);
        if (!role)
            return Result<bool>.AlreadyExists("Role");

        return Result<bool>.Success(role);
    }

    public async Task<Result<bool>> ExistsRoleByNameValidator(
       string name)
    {
        var role = await _roleExistsRepository.ExistsAsync(name);
        if (!role)
            return Result<bool>.AlreadyExists("Role");

        return Result<bool>.Success(role);
    }
}