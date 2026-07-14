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
    private readonly IUserRepository _repository;
    private readonly UserUpdateValidator _updateValidator;
    private readonly UserValidator _validator;

    private readonly IUnitOfWork _unitOfWork;


    public UserUpdateService(
        IUserRepository userRepository,
        UserValidator validator,
        UserUpdateValidator updateValidator,
        IUnitOfWork unitOfWork)
    {
        _repository = userRepository;
        _validator = validator;
        _updateValidator = updateValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<UserUpdateResponse>> UpdateUserAsync(
        int userId,
        UserUpdateRequest request)
    {
        var user = await _validator.GetUserValidator(userId);
        if (!user.IsSuccess)
            return Result<UserUpdateResponse>.Fail(user.Error ?? "");

        var phone = await _updateValidator.ExistsPhoneValidator(request);
        if (!phone.IsSuccess)
            return Result<UserUpdateResponse>.Fail(phone.Error ?? "");

        var email = await _updateValidator.ExistsEmailValidator(request);
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
            _repository.Update(user.Value!);

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