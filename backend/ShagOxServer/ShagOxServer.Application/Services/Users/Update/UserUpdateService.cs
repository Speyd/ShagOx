using ShagOxServer.Application.DTOs.Users.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Services.Users.Update;
using ShagOxServer.Application.Services.Users.Update.Validator;
using ShagOxServer.Application.Services.Users.Validator;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Users.Update;
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


    public async Task<Result<UserUpdateResponse>> UpdateAsync(
        int userId,
        UserUpdateRequest request)
    {
        var user = await _userValidator.GetByIdAsync(userId);
        if (!user.IsSuccess)
            return Result<UserUpdateResponse>.Fail(user.Error ?? "");

        var phone = await _userUpdateValidator.ExistsPhoneValidator(request);
        if (!phone.IsSuccess)
            return Result<UserUpdateResponse>.Fail(phone.Error ?? "");

        var email = await _userUpdateValidator.ExistsEmailValidator(request);
        if (!email.IsSuccess)
            return Result<UserUpdateResponse>.Fail(email.Error ?? "");

        var updatedCount = UserUpdater.ApplyUpdates(user.Value!, request);
        var result = new UserUpdateResponse(
            DateTime.UtcNow,
            updatedCount
            );

        if (updatedCount == 0)
            return Result<UserUpdateResponse>.Success(result);

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

        return Result<UserUpdateResponse>.Success(result);
    }
}