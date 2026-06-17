using Microsoft.AspNetCore.Identity;
using ShagOxServer.Application.DTOs.Auth;
using ShagOxServer.Application.Interfaces;
using ShagOxServer.Application.Validators;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Infrastructure.Interfaces;
namespace ShagOxServer.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IContactValidator _contactValidator;

    public AuthService(
    IUserRepository repository,
    IPasswordHasher<User> passwordHasher,
    IContactValidator contactValidator)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
        _contactValidator = contactValidator;
    }


    public async Task<RegisterResponse> RegisterAsync(
        RegisterRequest request)
    {
        var user = new User();


        var contactType =
            ApplyContact(
                user,
                request
            );


        user.PasswordHash =
            _passwordHasher.HashPassword(
                user,
                request.Password
            );


        await _repository.AddAsync(user);


        if (string.IsNullOrEmpty(user.Name))
        {
            user.Name = $"user-{user.Id}";

            await _repository.UpdateAsync(user);
        }


        var contact = GetContact(
            user,
            contactType
        );


        return new RegisterResponse(
            user.Id,
            contact,
            user.Name
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