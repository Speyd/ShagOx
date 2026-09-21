using ShagOxServer.Application.DTOs.Auth.Register;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Services.Auth.Users.Contacts;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Core.Create;
public class UserCreater
{
    private readonly IRepository<User> _repository;

    private readonly UserContactApplier _contactApplier;


    public UserCreater(
        IRepository<User> repository,
        UserContactApplier contactApplier)
    {
        _repository = repository;
        _contactApplier = contactApplier;
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