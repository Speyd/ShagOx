using Microsoft.AspNetCore.Identity;
using ShagOxServer.Application.Common.Validators;
using ShagOxServer.Application.DTOs.Auth.Register;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Services.Common.Validators;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Auth;
public class UserCreater
{
    private readonly IUserRepository _userRepository;

    private readonly IRoleQueryRepository _roleQueryRepository;

    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IContactValidator _contactValidator;


    public UserCreater(
        IUserRepository userRepository,
        IRoleQueryRepository roleQueryRepository,
        IPasswordHasher<User> passwordHasher,
        IContactValidator contactValidator)
    {
        _userRepository = userRepository;
        _roleQueryRepository = roleQueryRepository;
        _passwordHasher = passwordHasher;
        _contactValidator = contactValidator;
    }

    public User CreateUser(RegisterRequest request)
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

    public async Task AddDefaultRole(User user)
    {
        var role =
            await _roleQueryRepository.GetByNameAsync("User");


        if (role == null)
            throw new Exception("User role not found");


        user.UserRoles.Add(
            new UserRole
            {
                RoleId = role.Id,
            });
    }

    public async Task SetDefaultName(User user)
    {
        if (!string.IsNullOrEmpty(user.Name))
            return;


        user.Name = $"user-{user.Id}";


        _userRepository.Update(user);
    }

    public UserContactType ApplyContact(
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