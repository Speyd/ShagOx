using Microsoft.AspNetCore.Identity;
using ShagOxServer.Application.Common.Validators;
using ShagOxServer.Application.DTOs.Auth.Register;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Common.Validators;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Auth.Users.Create;
public class UserCreater
{
    private readonly IRepository<User> _userRepository;

    private readonly IRoleQueryRepository _roleQueryRepository;

    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IContactValidator _contactValidator;
    private readonly IUnitOfWork _unitOfWork;

    private readonly string DefaultRole = "User";


    public UserCreater(
        IRepository<User> userRepository,
        IRoleQueryRepository roleQueryRepository,
        IPasswordHasher<User> passwordHasher,
        IContactValidator contactValidator,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _roleQueryRepository = roleQueryRepository;
        _passwordHasher = passwordHasher;
        _contactValidator = contactValidator;
        _unitOfWork = unitOfWork;
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