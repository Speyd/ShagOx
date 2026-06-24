using Microsoft.AspNetCore.Identity;
using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.Common.Validators;
using ShagOxServer.Application.DTOs.Auth.Register;
using ShagOxServer.Application.Interfaces.Auth;
using ShagOxServer.Application.Interfaces.Validators;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Interfaces.Auth;
namespace ShagOxServer.Application.Services.Auth;

public class RegisterService : IRegisterService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IContactValidator _contactValidator;


    public RegisterService(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IPasswordHasher<User> passwordHasher,
        IContactValidator contactValidator)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _passwordHasher = passwordHasher;
        _contactValidator = contactValidator;
    }


    public async Task<Result<RegisterResponse>> RegisterAsync(
        RegisterRequest request)
    {
        try
        {
            var user = CreateUser(request);

            var exists = await _userRepository.ExistsAsync(user.Email, user.Phone);

            if (exists)
                return Result<RegisterResponse>.Fail("User already exists");


            await AddDefaultRole(user);

            await _userRepository.AddAsync(user);

            await SetDefaultName(user);

            return Result<RegisterResponse>.Success(
               new RegisterResponse(user)
            );
        }
        catch (Exception ex)
        {
            _ = ex;
            return Result<RegisterResponse>.Fail("Unknown Exception");
        }
    }
    private User CreateUser(RegisterRequest request)
    {
        var user = new User();

        ApplyContact(user, request);


        user.PasswordHash =
            _passwordHasher.HashPassword(
                user,
                request.Password
            );

        return user;
    }

    private async Task AddDefaultRole(User user)
    {
        var role =
            await _roleRepository.GetByNameAsync("User");


        if (role == null)
            throw new Exception("User role not found");


        user.UserRoles.Add(
            new UserRole
            {
                RoleId = role.Id,
                Role = role
            });
    }

    private async Task SetDefaultName(User user)
    {
        if (!string.IsNullOrEmpty(user.Name))
            return;


        user.Name = $"user-{user.Id}";


        await _userRepository.UpdateAsync(user);
    }

    private UserContactType ApplyContact(
        User user,
        RegisterRequest request)
    {
        var data = request.EmailOrPhone;


        var type =
            _contactValidator.Detect(data);


        switch (type)
        {
            case UserContactType.Email:
                user.Email = data;
                user.Name =
                    data.Split('@')[0];

                break;

            case UserContactType.Phone:
                user.Phone = data;

                break;
        }


        return type;
    }
}
