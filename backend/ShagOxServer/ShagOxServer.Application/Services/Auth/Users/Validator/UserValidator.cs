using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Validator;
public class UserValidator
{
    private readonly IUserRepository _userRepository;
    private readonly IUserExistsRepository _userExistsRepository;


    public UserValidator(
        IUserRepository userRepository, 
        IUserExistsRepository userExistsRepository)
    {
        _userRepository = userRepository;
        _userExistsRepository = userExistsRepository;
    }


    public async Task<Result<User>> GetByIdAsync(
        int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
            return Result<User>.NotFound("User");

        return Result<User>.Success(user);
    }

    public async Task<Result<bool>> ExistsByIdAsync(
       int userId)
    {
        if (!await _userExistsRepository.ExistsByIdAsync(userId))
            return Result<bool>.NotFound("User");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByIdAsync(
       int userId)
    {
        if (await _userExistsRepository.ExistsByIdAsync(userId))
            return Result<bool>.AlreadyExists("User");

        return Result<bool>.Success(true);
    }
}