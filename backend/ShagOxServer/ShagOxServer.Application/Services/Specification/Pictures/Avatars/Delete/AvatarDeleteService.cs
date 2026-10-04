using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
using ShagOxServer.Application.Services.Specification.Pictures.Avatars.Mapping;
using ShagOxServer.Application.Services.Specification.Pictures.Avatars.Validator;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Avatars.Delete;

public class AvatarDeleteService
    : IAvatarDeleteService
{
    private readonly IRepository<Avatar> _avatarRepository;
    private readonly AvatarValidator _avatarValidator;
    private readonly IPictureLoaderService _loaderService;

    private readonly AvatarInvalidationService _avatarInvalid;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AvatarDeleteService> _logger;


    public AvatarDeleteService(
        IRepository<Avatar> avatarRepository,
        AvatarValidator avatarValidator,
        IPictureLoaderService loaderService,
        AvatarInvalidationService avatarInvalid,
        IUnitOfWork unitOfWork,
        ILogger<AvatarDeleteService> logger)
    {
        _avatarRepository = avatarRepository;
        _avatarValidator = avatarValidator;
        _loaderService = loaderService;
        _avatarInvalid = avatarInvalid;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        long id)
    {
        var avatar = await _avatarValidator
            .GetByIdAsync(id);

        if (!avatar.IsSuccess)
            return Result<DeleteResponse>
                .Fail(avatar.Error);


        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var result = await DeleteInternalAsync(
                avatar.Value!);

            if (!result.IsSuccess)
            {
                await _unitOfWork.RollbackAsync();

                return result;
            }

            await _unitOfWork.CommitAsync();

            await _avatarInvalid.InvalidateDeleteAsync(
                AvatarCacheMapper.ToInfo(avatar.Value!));

            return result;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to delete avatar. Id: {Id}",
                id);

            return Result<DeleteResponse>
                .Fail(EntityErrorResources.AvatarDeleteFailed);
        }
    }


    public async Task<Result<DeleteResponse>> DeleteInternalAsync(
        Avatar avatar)
    {
        _avatarRepository.Delete(avatar);

        var result = await _loaderService
            .DeleteAsync(avatar.PublicId);

        if (!result.IsSuccess)
        {
            return Result<DeleteResponse>
                .Fail(result.Error);
        }

        _logger.LogInformation(
            "Avatar deleted successfully. Id: {Id}",
            avatar.Id);

        return Result<DeleteResponse>.Success(
            new DeleteResponse(
                avatar.Id,
                DateTime.UtcNow));
    }
}