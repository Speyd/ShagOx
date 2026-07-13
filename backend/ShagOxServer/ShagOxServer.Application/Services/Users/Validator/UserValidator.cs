using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Users.Validator;
public class UserValidator
{
    private readonly IUserRepository _userRepository;


    public UserValidator(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<User>> GetUserValidator(
        int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
            return Result<User>.NotFound("User");

        return Result<User>.Success(user);
    }
}