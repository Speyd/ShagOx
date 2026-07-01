using Microsoft.AspNetCore.Identity;
using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.Common.Validators;
using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.Interfaces.Auth;
using ShagOxServer.Application.Interfaces.Common.Validators;
using ShagOxServer.Application.Interfaces.Jwt;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Interfaces.Auth.Users;

namespace ShagOxServer.Application.Services.Auth;
public class LoginService : ILoginService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IContactValidator _contactValidator;
    private readonly IJwtService _jwtService;

    public LoginService(
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher,
        IContactValidator contactValidator,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _contactValidator = contactValidator;
        _jwtService = jwtService; 
    }

    public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request)
    {
        try
        {
            var type = _contactValidator.Detect(request.EmailOrPhone);

            var user = await GetUserAsync(request.EmailOrPhone, type);

            if (user is null)
                return Result<LoginResponse>.Fail("User not found");

            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                request.Password
            );

            if (result == PasswordVerificationResult.Failed)
                return Result<LoginResponse>.Fail("Invalid password");

            return Result<LoginResponse>.Success(
                new LoginResponse(_jwtService.GenerateToken(user))
            );
        }
        catch (Exception ex)
        {
            return Result<LoginResponse>.Fail(ex.Message);
        }
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
}
