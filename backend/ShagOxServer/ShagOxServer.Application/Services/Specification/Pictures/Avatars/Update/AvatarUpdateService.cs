using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Update;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Update;
using ShagOxServer.Application.Services.Auth.Users.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Avatars.Validator;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Avatars.Update;
public class AvatarUpdateService
    : IAvatarUpdateService
{
    private readonly IRepository<Avatar> _avatarRepository;
    private readonly AvatarValidator _avatarValidator;
    private readonly UserValidator _userValidator;


    private readonly IUnitOfWork _unitOfWork;


    public AvatarUpdateService(
        IRepository<Avatar> avatarRepository,
        AvatarValidator avatarValidator,
        UserValidator userValidator,
        IUnitOfWork unitOfWork)
    {
        _avatarRepository = avatarRepository;
        _avatarValidator = avatarValidator;
        _userValidator = userValidator;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<UpdateResponse>> UpdateAsync(
        int avatarId,
        AvatarUpdateRequest request)
    {
        var avatar = await _avatarValidator.GetByIdAsync(avatarId);
        if (!avatar.IsSuccess)
            return Result<UpdateResponse>.Fail(avatar.Error);


        var validation = await
             ValidateUpdatesAsync(avatar.Value!, request);

        if (!validation.IsSuccess)
            return Result<UpdateResponse>.Fail(validation.Error);


        var updatedCount = AvatarUpdater
            .ApplyUpdates(avatar.Value!, request);

        var result = new UpdateResponse(
            updatedCount,
            DateTime.UtcNow
        );

        if (updatedCount == 0)
            return Result<UpdateResponse>.Success(result);


        await _unitOfWork.BeginTransactionAsync();

        try
        {
            _avatarRepository.Update(avatar.Value!);

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }


        return Result<UpdateResponse>.Success(result);
    }

    private async Task<Result<bool>> ValidateUpdatesAsync(
        Avatar avatar,
        AvatarUpdateRequest request)
    {
        if(request.UserId is null ||
           avatar.UserId == request.UserId)
            return Result<bool>.Success(true);

        var existsValidator = await _userValidator.ExistsByIdAsync(
            request.UserId.Value
        );

        if (!existsValidator.IsSuccess)
            return Result<bool>.Fail(existsValidator.Error);

        return Result<bool>.Success(true);
    }
}