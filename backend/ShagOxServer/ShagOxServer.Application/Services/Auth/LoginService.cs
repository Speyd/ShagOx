using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Common.Validators.Enum;
using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Auth;
using ShagOxServer.Application.Interfaces.Services.Common.Validators;
using ShagOxServer.Application.Interfaces.Services.Jwt;
using ShagOxServer.Application.Services.Auth.Users.Core.Create;
using ShagOxServer.Application.Services.Auth.Users.Roles;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Account.Enum;
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
    private readonly IUnitOfWork _unitOfWork;

    public LoginService(
        IRepository<User> userRepository,
        IUserQueryRepository userQueryRepository,
        UserCreater userCreater,
        UserRoleService roleService,
        IPasswordHasher<User> passwordHasher,
        IContactValidator contactValidator,
        IJwtService jwtService,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _userQueryRepository = userQueryRepository;
        _userCreater = userCreater;
        _roleService = roleService;
        _passwordHasher = passwordHasher;
        _contactValidator = contactValidator;
        _jwtService = jwtService;
        _unitOfWork = unitOfWork;
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

    public async Task<Result<LoginResponse>> GoogleLoginAsync(string email, string name, string googleId)
    {
        try
        {
            var user = await _userQueryRepository.GetByEmailAsync(email);

            if (user is null)
            {
                user = new User
                {
                    Email = email,
                    UserName = string.IsNullOrWhiteSpace(name) ? email.Split('@')[0] : name,
                    Status = UserStatus.Active
                };

                await _unitOfWork.BeginTransactionAsync();

                try
                {
                    _userRepository.Add(user);

                    var roleResult = await _roleService.AddDefaultRoleAsync(user);
                    if (!roleResult.IsSuccess)
                    {
                        await _unitOfWork.RollbackAsync();
                        return Result<LoginResponse>.Fail(roleResult.Error);
                    }

                    await _unitOfWork.SaveChangesAsync();
                    await _userCreater.SetDefaultName(user);
                    await _unitOfWork.CommitAsync();
                }
                catch
                {
                    await _unitOfWork.RollbackAsync();
                    throw;
                }
            }

            return Result<LoginResponse>.Success(
                new LoginResponse(_jwtService.GenerateToken(user))
            );
        }
        catch (Exception ex)
        {
            return Result<LoginResponse>.Fail(ex.Message);
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