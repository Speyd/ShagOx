using Microsoft.AspNetCore.Identity;
using ShagOxServer.Application.DTOs.Auth.Register;
using ShagOxServer.Application.Resources.Auth.Contacts.Passwords;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Contacts.Passwords;
public class UserPasswordService
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserPasswordService(
        IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public Result<bool> Apply(
        User user,
        RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Password))
        {
            return Result<bool>
                .Fail(PasswordAuth.InvalidNewPassword);
        }

        user.PasswordHash =
            _passwordHasher.HashPassword(
                user,
                request.Password);

        return Result<bool>.Success(true);
    }
}