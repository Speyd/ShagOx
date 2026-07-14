using ShagOxServer.Application.DTOs.Users.Update;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Users.Update.Validator;
public class UserUpdateValidator
{
    private readonly IUserExistsRepository _existsRepository;

    public UserUpdateValidator(
        IUserExistsRepository existsRepository)
    {
        _existsRepository = existsRepository;
    }

    public async Task<Result<bool>> ExistsPhoneValidator(
       UserUpdateRequest request)
    {
        if(request.Phone is null)
            return Result<bool>.Success(true);

        var exists = await _existsRepository.ExistsPhoneAsync(request.Phone);
        if (exists)
            return Result<bool>.AlreadyExists("Phone");

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ExistsEmailValidator(
       UserUpdateRequest request)
    {
        if (request.Email is null)
            return Result<bool>.Success(true);

        var exists = await _existsRepository.ExistsEmailAsync(request.Email);
        if (exists)
            return Result<bool>.AlreadyExists("Phone");

        return Result<bool>.Success(true);
    }
}