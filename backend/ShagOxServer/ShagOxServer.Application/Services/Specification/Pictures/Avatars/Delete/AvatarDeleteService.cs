using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Delete;
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


    public AvatarDeleteService(
        IRepository<Avatar> avatarRepository,
        AvatarValidator avatarValidator,
        IPictureLoaderService loaderService)
    {
        _avatarRepository = avatarRepository;
        _avatarValidator = avatarValidator;
        _loaderService = loaderService;
    }


    public async Task<Result<DeleteResponse>> DeleteAsync(
        int id)
    {
        var avatar = await _avatarValidator.GetByIdAsync(id);
        if (!avatar.IsSuccess)
            return Result<DeleteResponse>.Fail(avatar.Error);

        _avatarRepository.Delete(avatar.Value!);

        await _loaderService.DeleteAsync(avatar.Value!.PublicId);

        return Result<DeleteResponse>.Success(
           new DeleteResponse(
               avatar.Value!.Id,
               DateTime.UtcNow
           )
       );
    }
}