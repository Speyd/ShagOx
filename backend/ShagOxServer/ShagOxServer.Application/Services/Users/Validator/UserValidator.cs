using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Users.Validator;
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

    public async Task<Result<User>> GetUserValidator(
        int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
            return Result<User>.NotFound("User");

        return Result<User>.Success(user);
    }

    public async Task<Result<bool>> ExistsUserValidator(
       int userId)
    {
        var user = await _userExistsRepository.ExistsAsync(userId);
        if (user)
            return Result<bool>.AlreadyExists("User");

        return Result<bool>.Success(user);
    }
}