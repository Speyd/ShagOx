using ShagOxServer.Application.DTOs.Auth.Register;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Services.Auth.Users.Contacts;
using ShagOxServer.Application.Services.Auth.Users.Contacts.Passwords;
using ShagOxServer.Application.Services.Auth.Users.Roles;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Core.Create;
public class UserCreater
{
    private readonly IRepository<User> _repository;

    private readonly UserContactApplier _contactApplier;
    private readonly UserPasswordService _passwordService;
    private readonly UserRoleService _roleService;


    public UserCreater(
        IRepository<User> repository,
        UserContactApplier contactApplier,
        UserPasswordService passwordService,
        UserRoleService roleService)
    {
        _repository = repository;
        _contactApplier = contactApplier;
        _passwordService = passwordService;
        _roleService = roleService;
    }

    public async Task<Result<User>> CreateUser(RegisterRequest request)
    {
        var user = new User();
       
        var contactResult =
            await _contactApplier.ApplyAsync(user, request);
        
        if (!contactResult.IsSuccess)
            return Result<User>.Fail(contactResult.Error);


        return Result<User>.Success(user);
    }

    public async Task SetDefaultName(
        User user)
    {
        if (!string.IsNullOrEmpty(user.Email))
            user.FirstName = user.Email.Split('@')[0];
        else
            user.FirstName = $"user-{user.Id}";

        _repository.Update(user);
    }
}