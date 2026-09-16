using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using ShagOxServer.Application.Common.Validators.Enum;
using ShagOxServer.Application.DTOs.Auth.Register;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Common.Validators;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Core.Create;
public class UserCreater
{
    private readonly IRepository<User> _userRepository;
    private readonly UserValidator _userValidator;


    private readonly IRoleQueryRepository _roleQueryRepository;

    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IContactValidator _contactValidator;
    private readonly IUnitOfWork _unitOfWork;

    private readonly string DefaultRole = "User";


    public UserCreater(
        IRepository<User> userRepository,
        UserValidator userValidator,
        IRoleQueryRepository roleQueryRepository,
        IPasswordHasher<User> passwordHasher,
        IContactValidator contactValidator,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
        _roleQueryRepository = roleQueryRepository;
        _passwordHasher = passwordHasher;
        _contactValidator = contactValidator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<User>> CreateUser(
        RegisterRequest request)
    {
        var user = new User();

        var applyResult = await ApplyContact(user, request);
        if (!applyResult.IsSuccess)
            Result<User>.Fail(applyResult.Error);

        return Result<User>.Success(user);
    }

    public Result<bool> CreatePasswordHash(
        User user,
        RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Password))
            return Result<bool>.Fail("New Password is incorrect!");

        user.PasswordHash =
            _passwordHasher.HashPassword(
                user,
                request.Password
            );

        return Result<bool>.Success(true);
    }

    public async Task AddDefaultRole(
        User user)
    {
        var role = await _roleQueryRepository
            .GetByNameAsync(DefaultRole);

        if (role is null)
            throw new Exception("Role not found");


        user.UserRoles.Add(new UserRole
        {
            Role = role
        });

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task SetDefaultName(
        User user)
    {
        if (!string.IsNullOrEmpty(user.FirstName))
            return;


        user.FirstName = $"user-{user.Id}";


        _userRepository.Update(user);
    }

    public async Task<Result<UserContactType>> ApplyContact(
        User user,
        RegisterRequest request)
    {
        var type =
            _contactValidator.Detect(request.EmailOrPhone);


        switch (type)
        {
            case UserContactType.Email:
                var email = await ApplyEmail(user, request);
                if (!email.IsSuccess)
                {
                    return Result<UserContactType>
                        .Fail(email.Error);
                }

                break;

            case UserContactType.Phone:
                var phone = await ApplyPhone(user, request);
                if (!phone.IsSuccess)
                {
                    return Result<UserContactType>
                        .Fail(phone.Error);
                }

                break;

            default:
                return Result<UserContactType>
                    .Fail("Unsupported contact type.");
        }

        var userName = await ApplyUserName(user, request);
        if (!userName.IsSuccess)
        {
            return Result<UserContactType>
                .Fail(userName.Error);
        }

        return Result<UserContactType>.Success(type);
    }

    public async Task<Result<bool>> ApplyEmail(
        User user,
        RegisterRequest request)
    {
        var data = request.EmailOrPhone;

        if (user.Email != data)
        {
            var result = await _userValidator
                .NotExistsByEmailAsync(data);
            if (!result.IsSuccess)
                return Result<bool>.Fail(result.Error);        
        }

        user.Email = data;
        user.FirstName =
            data.Split('@')[0];

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ApplyPhone(
        User user,
        RegisterRequest request)
    {
        var data = request.EmailOrPhone;

        if (user.Phone != data)
        {
            var result = await _userValidator
                .NotExistsByPhoneAsync(data);
            if (!result.IsSuccess)
                return Result<bool>.Fail(result.Error);
        }

        user.Phone = data;

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ApplyUserName(
        User user,
        RegisterRequest request)
    {
        var data = request.UserName;

        if (user.UserName != data)
        {
            var result = await _userValidator
                .NotExistsByUserNameAsync(data);
            if (!result.IsSuccess)
                return Result<bool>.Fail(result.Error);
        }

        user.UserName = data;

        return Result<bool>.Success(true);
    }
}