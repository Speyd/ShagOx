using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.Common.Validators.Enum;
using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Auth.Login;
using ShagOxServer.Application.Interfaces.Services.Common.Validators;
using ShagOxServer.Application.Resources.Auth.Logins;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Core.Login;
public class LoginService 
    : ILoginService
{
    private readonly IRepository<User> _userRepository;
    private readonly IUserQueryRepository _userQueryRepository;

    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IContactValidator _contactValidator;

    private readonly ILoginPolicy _loginPolicy;
    private readonly ILoginSessionService _sessionService;
    
    private readonly ILogger<LoginService> _logger;
    private readonly IUnitOfWork _unitOfWork;


    public LoginService(
        IRepository<User> userRepository,
        IUserQueryRepository userQueryRepository,
        IPasswordHasher<User> passwordHasher,
        IContactValidator contactValidator,
        ILoginPolicy loginPolicy,
        ILoginSessionService sessionService,
        ILogger<LoginService> logger,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _userQueryRepository = userQueryRepository;
        _passwordHasher = passwordHasher;
        _contactValidator = contactValidator;
        _loginPolicy = loginPolicy;
        _sessionService = sessionService;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<LoginResponse>> LoginAsync(
        LoginRequest request)
    {
        try
        {
            var user = await FindUserAsync(
                request.EmailOrPhoneOrUserName);

            if (user is null)
                return InvalidCredentials();

            if (!_loginPolicy.CanLogin(user))
            {
                return Result<LoginResponse>.Fail(
                    LoginAuthResources.LoginNotAllowed);
            }

            var verification = _passwordHasher
                .VerifyHashedPassword(
                    user,
                    user.PasswordHash,
                    request.Password);

            if (verification == PasswordVerificationResult.Failed)
                return InvalidCredentials();

            if (verification ==
                PasswordVerificationResult.SuccessRehashNeeded)
            {
                var newHash = _passwordHasher.HashPassword(
                    user,
                    request.Password);

                user.PasswordHash = newHash;

                _userRepository.Add(user);

                await _unitOfWork.SaveChangesAsync();
            }

            return await _sessionService.CreateSessionAsync(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Login operation failed.");

            return Result<LoginResponse>.Fail(
                LoginAuthResources.LoginFailed);
        }
    }

    private async Task<User?> FindUserAsync(
        string contact)
    {
        var type = _contactValidator.Detect(contact);

        return type switch
        {
            UserContactType.Email =>
                await _userQueryRepository.GetByEmailAsync(contact),

            UserContactType.Phone =>
                await _userQueryRepository.GetByPhoneAsync(contact),

            UserContactType.UserName =>
                await _userQueryRepository.GetByUserNameAsync(contact),

            _ => null
        };
    }

    private static Result<LoginResponse> InvalidCredentials()
    {
        return Result<LoginResponse>.Fail(
            LoginAuthResources.InvalidCredentials);
    }
}
