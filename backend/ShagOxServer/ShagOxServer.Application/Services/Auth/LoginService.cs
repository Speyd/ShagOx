using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.Common.Validators.Enum;
using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Auth;
using ShagOxServer.Application.Interfaces.Services.Common.Validators;
using ShagOxServer.Application.Interfaces.Services.Jwt;
using ShagOxServer.Application.Resources.Auth.Contacts.Passwords;
using ShagOxServer.Application.Resources.Auth.Logins;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth;

public class LoginService : ILoginService
{
    private readonly IRepository<User> _userRepository;
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly UserCreater _userCreater;
    private readonly UserRoleService _roleService;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IContactValidator _contactValidator;
    private readonly IJwtService _jwtService;

    private readonly ILogger<LoginService> _logger;


    public LoginService(
        IRepository<User> userRepository,
        IUserQueryRepository userQueryRepository,
        UserCreater userCreater,
        UserRoleService roleService,
        IPasswordHasher<User> passwordHasher,
        IContactValidator contactValidator,
        IJwtService jwtService,

        ILogger<LoginService> logger)
    {
        _userRepository = userRepository;
        _userQueryRepository = userQueryRepository;
        _userCreater = userCreater;
        _roleService = roleService;
        _passwordHasher = passwordHasher;
        _contactValidator = contactValidator;
        _jwtService = jwtService;
        _logger = logger;
    } 


    public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request)
    {
        try
        {
            var type = _contactValidator.Detect(request.EmailOrPhoneOrUserName);

            var user = await GetUserAsync(request.EmailOrPhoneOrUserName, type);
            if (user is null)
                return Result<LoginResponse>.NotFound(typeof(User));

            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                request.Password
            );

            if (result == PasswordVerificationResult.Failed)
            {
                return Result<LoginResponse>
                    .Fail(PasswordAuthResources.InvalidPassword);
            }

            _logger.LogInformation(
                 "User logged in successfully. UserId: {UserId}",
                 user.Id);

            return Result<LoginResponse>.Success(
                new LoginResponse(_jwtService.GenerateToken(user))
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Login failed. Contact: {Contact}",
                request.EmailOrPhoneOrUserName);

            return Result<LoginResponse>
                .Fail(LoginAuthResources.LoginFailed);
        }
    }


    private async Task<User?> GetUserAsync(
        string data,
        UserContactType type)
    {
        return type switch
        {
            UserContactType.Email => await _userQueryRepository.GetByEmailAsync(data),
            UserContactType.Phone => await _userQueryRepository.GetByPhoneAsync(data),
            UserContactType.UserName => await _userQueryRepository.GetByUserNameAsync(data),
            _ => null
        };
    }
}