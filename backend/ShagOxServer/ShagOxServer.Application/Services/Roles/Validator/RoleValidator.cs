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


    public async Task<Result<Role>> GetByIdAsync(
        int roleId)
    {
        var role = await _roleRepository.GetByIdAsync(roleId);
        if (role is null)
            return Result<Role>.NotFound("Role");

        return Result<Role>.Success(role);
    }

    public async Task<Result<bool>> ExistsByIdAsync(
       int roleId)
    {
        if (!await _roleExistsRepository.ExistsAsync(roleId))
            return Result<bool>.NotFound("Role");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByIdAsync(
       int roleId)
    {
        if (await _roleExistsRepository.ExistsAsync(roleId))
            return Result<bool>.AlreadyExists("Role");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByNameAsync(
       string name)
    {
        if (!await _roleExistsRepository.ExistsAsync(name))
            return Result<bool>.NotFound("Role");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByNameAsync(
      string name)
    {
        if (await _roleExistsRepository.ExistsAsync(name))
            return Result<bool>.AlreadyExists("Role");

        return Result<bool>.Success(true);
    }
}