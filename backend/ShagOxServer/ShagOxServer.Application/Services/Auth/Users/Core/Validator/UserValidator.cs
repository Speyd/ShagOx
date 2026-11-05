using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Core.Validator;
public class UserValidator
    : BaseValidator<User>
{
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IUserExistsRepository _userExistsRepository;


    public UserValidator(
        IRepository<User> repository,
        IQueryRepository<User> userRepository,
        IUserQueryRepository userQueryRepository,
        IUserExistsRepository userExistsRepository,
        IDataCacheService cache
    ) 
        : base(repository, userRepository, userExistsRepository, cache)
    {
        _userQueryRepository = userQueryRepository;
        _userExistsRepository = userExistsRepository;
    }

    public async Task<Result<bool>> ExistsByPhoneAsync(
      string phone,
        CancellationToken cancellationToken = default)
    {
        if (!await _userExistsRepository
                .ExistsByPhoneAsync(phone, cancellationToken))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.User);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByPhoneAsync(
       string phone,
        CancellationToken cancellationToken = default)
    {
        if (await _userExistsRepository
                .ExistsByPhoneAsync(phone, cancellationToken))
        {
            return Result<bool>.AlreadyExists(
                EntityNamesResources.User);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByEmailAsync(
      string email,
        CancellationToken cancellationToken = default)
    {
        if (!await _userExistsRepository
                .ExistsByEmailAsync(email, cancellationToken))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.User);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByEmailAsync(
       string email,
        CancellationToken cancellationToken = default)
    {
        if (await _userExistsRepository
            .ExistsByEmailAsync(email, cancellationToken))
        {
            return Result<bool>.AlreadyExists(
                EntityNamesResources.User);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByUserNameAsync(
      string userName,
        CancellationToken cancellationToken = default)
    {
        if (!await _userExistsRepository
                .ExistsByUserNameAsync(userName, cancellationToken))
        {
            return Result<bool>.NotFound(
                EntityNamesResources.User);
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByUserNameAsync(
       string userName,
        CancellationToken cancellationToken = default)
    {
        if (await _userExistsRepository
            .ExistsByUserNameAsync(userName, cancellationToken))
        {
            return Result<bool>.AlreadyExists(
                EntityNamesResources.User);
        }

        return Result<bool>.Success(true);
    }
}
