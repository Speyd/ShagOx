using ShagOxServer.Application.DTOs.Auth.Users.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Update;
using ShagOxServer.Application.Services.Auth.Users.Update.Validator;
using ShagOxServer.Application.Services.Auth.Users.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.Application.DTOs.Common.Responses;

namespace ShagOxServer.Application.Services.Auth.Users.Update;
public class UserUpdateService : IUserUpdateService
{
    private readonly IUserRepository _userRepository;
    private readonly UserValidator _userValidator;
    private readonly UserUpdateValidator _userUpdateValidator;

    private readonly IUnitOfWork _unitOfWork;


    public UserUpdateService(
        IUserRepository userRepository,
        UserValidator userValidator,
        UserUpdateValidator userUpdateValidator,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _userValidator = userValidator;
        _userUpdateValidator = userUpdateValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int userId,
        UserUpdateRequest request)
    {
        var user = await _userValidator
            .GetByIdAsync(userId);

        if (!user.IsSuccess)
            return Result<UpdateResponse>.Fail(user.Error);


        var phone = await _userUpdateValidator
            .ExistsPhoneValidator(request);

        if (!phone.IsSuccess)
            return Result<UpdateResponse>.Fail(phone.Error);


        var email = await _userUpdateValidator
            .ExistsEmailValidator(request);

        if (!email.IsSuccess)
            return Result<UpdateResponse>.Fail(email.Error);


        var updatedCount = UserUpdater
            .ApplyUpdates(user.Value!, request);

        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(result);

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _userRepository.Update(user.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }

        return Result<UpdateResponse>.Success(result);
    }
}