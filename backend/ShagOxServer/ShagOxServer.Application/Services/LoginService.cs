using Microsoft.AspNetCore.Identity;
using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.Interfaces;
using ShagOxServer.Application.Validators;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Interfaces;

namespace ShagOxServer.Application.Services;
public class LoginService : ILoginService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IContactValidator _contactValidator;


    public LoginService(
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher,
        IContactValidator contactValidator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _contactValidator = contactValidator;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var type = _contactValidator.Detect(request.EmailOrPhone);

        var user = await GetUserAsync(request.EmailOrPhone, type);

        if (user is null)
            return Fail("User not found");

        var result = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password
        );

        if (result == PasswordVerificationResult.Failed)
            return Fail("Invalid password");

        return Success("jwt", "Login successful");
    }

    private async Task<User?> GetUserAsync(string data, UserContactType type)
    {
        return type switch
        {
            UserContactType.Email => await _userRepository.GetByEmailAsync(data),
            UserContactType.Phone => await _userRepository.GetByPhoneAsync(data),
            _ => null
        };
    }

    public static LoginResponse Success(string token, string message)
       => new(true, token, message);

    public static LoginResponse Fail(string message)
        => new(false, null, message);
}
