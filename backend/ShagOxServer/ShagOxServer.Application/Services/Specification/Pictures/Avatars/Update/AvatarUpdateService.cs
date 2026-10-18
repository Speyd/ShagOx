using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Request;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Response;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Update;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
using ShagOxServer.Application.Services.Specification.Pictures.Avatars.Mapping;
using ShagOxServer.Application.Services.Specification.Pictures.Avatars.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Update;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Avatars.Update;
public class AvatarUpdateService
    : PictureUpdateService<User,
        Avatar,
        PictureUpdateRequest>,
    IAvatarUpdateService
{
    private readonly AvatarInvalidationService _avatarInvalid;


    public AvatarUpdateService(
        IRepository<Avatar> avatarRepository,
        PictureValidator pictureLoadValidator,
        AvatarValidator avatarValidator,
        UserValidator advertisementValidator,
        AvatarInvalidationService avatarInvalid,
        IUnitOfWork unitOfWork,
        ILogger<AvatarUpdateService> logger
    )
        : base(avatarRepository,
            pictureLoadValidator,
            avatarValidator,
            advertisementValidator,
            unitOfWork,
            logger)
    {
        _avatarInvalid = avatarInvalid;
    }


    public override async Task<Result<PictureUpdateResponse>> UpdateAsync(
        long avatarId,
        PictureUpdateRequest request)
    {
        try
        {

            return await base.UpdateAsync(avatarId, request);
        }
        catch
        {
            return Result<PictureUpdateResponse>.Fail(
                EntityErrorResources.AvatarUpdateFailed);
        }
    }

    public override async Task InvalidateCache(
        Avatar picture)
    {
        await _avatarInvalid.InvalidateUpdateAsync(
           AvatarCacheMapper.ToInfo(picture));
    }

    protected override int ApplyUpdates(
        Avatar picture,
        PictureUpdateRequest request,
        PictureLoaderUploadResponse? response)
    {
        if (response is not null)
        {
            return AvatarUpdater.ApplyUpdates(
                picture,
                request,
                response
            );
        }

        return AvatarUpdater.ApplyUpdates(
            picture,
            request
        );
    }
}
