using Microsoft.AspNetCore.Identity;
using ShagOxServer.Application.DTOs.Auth;
using ShagOxServer.Application.Interfaces;
using ShagOxServer.Application.Validators;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Interfaces;
namespace ShagOxServer.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IContactValidator _contactValidator;


    public AuthService(
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
        var user = CreateUser(request);

        await AddDefaultRole(user);

        await _userRepository.AddAsync(user);

        await SetDefaultName(user);

        return CreateResponse(user);
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

    private RegisterResponse CreateResponse(User user)
    {
        return new RegisterResponse(
            user.Id,
            user.Email ?? user.Phone!,
            user.Name!
        );
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

    private string GetContact(
        User user,
        UserContactType type)
    {
        return type switch
        {
            UserContactType.Email
                => user.Email!,

            UserContactType.Phone
                => user.Phone!,

            _ => throw new Exception(
                "Unknown contact type"
            )
        };
    }
}