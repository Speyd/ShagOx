using Microsoft.AspNetCore.Identity;
using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.DTOs.Auth.Register;
using ShagOxServer.Application.Interfaces;
using ShagOxServer.Application.Interfaces.Auth;
using ShagOxServer.Application.Validators;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Interfaces;
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


    public async Task<RegisterResponse> RegisterAsync(
        RegisterRequest request)
    {
        try
        {
            var user = CreateUser(request);

            var exists = await _userRepository.ExistsAsync(user.Email, user.Phone);

            if (exists)
                return Fail("User already exists");


            await AddDefaultRole(user);

            await _userRepository.AddAsync(user);

            await SetDefaultName(user);

            return Success(user, "Register successful");
        }
        catch (Exception ex)
        {
            return Fail("Unknown Exception");
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

    public static RegisterResponse Success(User user, string message)
       => new(
            user.Id,
            user.Email ?? user.Phone ?? "",
            user.Name ?? "",
            true,
            message
        );

    public static RegisterResponse Fail(string message, User? user = null)
        => new(
            user?.Id ?? -1, 
            user?.Email ?? user?.Phone ?? "Unknown Id", 
            user?.Name ?? "Unknown Name",
            false,
            message
        );
}
