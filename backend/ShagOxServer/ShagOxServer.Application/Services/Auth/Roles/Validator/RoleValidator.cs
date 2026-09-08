using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Roles.Validator;
public class RoleValidator
    : BaseValidator<Role>
{
    private readonly IRoleExistsRepository _roleExistsRepository;


    public RoleValidator(
        IRepository<Role> roleRepository,
        IRoleExistsRepository roleExistsRepository
    ) : base(roleRepository, roleExistsRepository)
    {
        _roleExistsRepository = roleExistsRepository;
    }


    public async Task<Result<bool>> ExistsByNameAsync(
       string name)
    {
        if (!await _roleExistsRepository
            .ExistsByNameAsync(name))
        {
            return Result<bool>
                .NotFound(typeof(Role));
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByNameAsync(
      string name)
    {
        if (await _roleExistsRepository
            .ExistsByNameAsync(name))
        {
            return Result<bool>
                .AlreadyExists(typeof(Role));
        }

        return Result<bool>.Success(true);
    }
}