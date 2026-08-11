using ShagOxServer.Application.DTOs.Auth.Users.Update;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Validator;
public class UserValidator
{
    private readonly IRepository<User> _userRepository;
    private readonly IUserExistsRepository _userExistsRepository;


    public UserValidator(
        IRepository<User> userRepository, 
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

    public async Task<Result<bool>> ExistsByPhoneAsync(
      string phone)
    {
        if (!await _userExistsRepository.ExistsPhoneAsync(phone))
            return Result<bool>.NotFound("User");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByPhoneAsync(
       string phone)
    {
        if (await _userExistsRepository.ExistsPhoneAsync(phone))
            return Result<bool>.AlreadyExists("User");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsByEmailAsync(
      string email)
    {
        if (!await _userExistsRepository.ExistsEmailAsync(email))
            return Result<bool>.NotFound("User");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> NotExistsByEmailAsync(
       string email)
    {
        if (await _userExistsRepository.ExistsEmailAsync(email))
            return Result<bool>.AlreadyExists("User");

        return Result<bool>.Success(true);
    }
}