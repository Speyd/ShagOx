using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
using ShagOxServer.Application.Services.Specification.Pictures.Avatars.Mapping;
using ShagOxServer.Application.Services.Specification.Pictures.Avatars.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Delete;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Avatars.Delete;
public class AvatarDeleteService
    : PictureDeleteService<User, Avatar>,
    IAvatarDeleteService
{
    private readonly AvatarInvalidationService _avatarInvalid;


    public AvatarDeleteService(
        IRepository<Avatar> avatarRepository,
        AvatarValidator avatarValidator,
        PictureValidator pictureValidator,
        AvatarInvalidationService avatarInvalid,
        IUnitOfWork unitOfWork,
        ILogger<AvatarDeleteService> logger
    )
        : base(avatarRepository, 
            avatarValidator, 
            pictureValidator, 
            unitOfWork, 
            logger)
    {
        _avatarInvalid = avatarInvalid;
    }


    public override async Task<Result<DeleteResponse>> DeleteAsync(
        long id)
    {
        try
        {
            return await base.DeleteAsync(id);
        }
        catch
        {

            return Result<DeleteResponse>
                .Fail(EntityErrorResources.AvatarDeleteFailed);
        }
    }
    public override async Task<Result<DeleteResponse>> DeleteRecordAsync(
        long id)
    {

        try
        {
            return await base.DeleteRecordAsync(id);
        }
        catch
        {
            return Result<DeleteResponse>
                .Fail(EntityErrorResources.AvatarDeleteFailed);
        }
    }


    public override async Task InvalidateCache(
        Avatar avatar)
    {
        await _avatarInvalid.InvalidateDeleteAsync(
            AvatarCacheMapper.ToInfo(avatar));
    }
}
