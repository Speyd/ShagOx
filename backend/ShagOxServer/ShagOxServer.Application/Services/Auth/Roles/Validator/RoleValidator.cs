using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Roles.Validator;
public class RoleValidator
    : BaseValidator<Role>
{
    private readonly IRoleExistsRepository _roleExistsRepository;


    public RoleValidator(
        IRepository<Role> repository,
        IQueryRepository<Role> roleRepository,
        IRoleExistsRepository roleExistsRepository,
        IDataCacheService cache
    ) 
        : base(repository, roleRepository, roleExistsRepository, cache)
    {
        _roleExistsRepository = roleExistsRepository;
    }


    public async Task<Result<bool>> ExistsByNameAsync(
       string name,
       CancellationToken cancellationToken = default)
    {
        if (!await _roleExistsRepository
            .ExistsByNameAsync(name, cancellationToken))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.Role);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByNameAsync(
      string name,
      CancellationToken cancellationToken = default)
    {
        if (await _roleExistsRepository
            .ExistsByNameAsync(name, cancellationToken))
        {
            return Result<bool>.AlreadyExists(
                EntityNamesResources.Role);
        }

        return Result<bool>.Success(true);
    }
}
