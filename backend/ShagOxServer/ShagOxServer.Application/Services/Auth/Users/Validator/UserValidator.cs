using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Validator;
public class UserValidator
    : BaseValidator<User>
{
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IUserExistsRepository _userExistsRepository;


    public UserValidator(
        IRepository<User> userRepository,
        IUserQueryRepository userQueryRepository,
        IUserExistsRepository userExistsRepository
    ) : base(userRepository, userExistsRepository)
    {
        _userQueryRepository = userQueryRepository;
        _userExistsRepository = userExistsRepository;
    }


    public async Task<Result<User>> GetByIdWithIncludesAsync(
        int userId)
    {
        var user = await _userQueryRepository.GetByIdAsync(userId);
        if (user is null)
            return Result<User>.NotFound(typeof(User));

        return Result<User>.Success(user);
    }

    public async Task<Result<bool>> ExistsByPhoneAsync(
      string phone)
    {
        if (!await _userExistsRepository
                .ExistsPhoneAsync(phone))
        {
            return Result<bool>
                .NotFound(typeof(User));
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByPhoneAsync(
       string phone)
    {
        if (await _userExistsRepository
                .ExistsPhoneAsync(phone))
        {
            return Result<bool>
                .AlreadyExists(typeof(User));
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByEmailAsync(
      string email)
    {
        if (!await _userExistsRepository
                .ExistsEmailAsync(email))
        {
            return Result<bool>
                .NotFound(typeof(User));
        }

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByEmailAsync(
       string email)
    {
        if (await _userExistsRepository
            .ExistsEmailAsync(email))
        {
            return Result<bool>
                .AlreadyExists(typeof(User));
        }

        return Result<bool>.Success(true);
    }
}