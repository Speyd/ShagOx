using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
using ShagOxServer.Application.Services.Specification.Pictures.Avatars.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Delete;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Avatars.Delete;
public class AvatarDeleteService
    : PictureDeleteService<Avatar>,
    IAvatarDeleteService
{
    public AvatarDeleteService(
        IRepository<Avatar> avatarRepository,
        ICommandRepository<Avatar> avatarCommand,
        AvatarValidator avatarValidator,
        PictureValidator pictureValidator,
        AvatarInvalidationService avatarInvalid,
        IUnitOfWork unitOfWork,
        ILogger<AvatarDeleteService> logger
    )
        : base(avatarRepository,
            avatarCommand,
            avatarValidator, 
            pictureValidator, 
            unitOfWork, 
            logger)
    {
    }


    public override async Task<Result<DeleteResponse>> DeleteAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await base.DeleteAsync(id, cancellationToken);
        }
        catch
        {

            return Result<DeleteResponse>
                .Fail(EntityErrorResources.AvatarDeleteFailed);
        }
    }
    public override async Task<Result<DeleteResponse>> DeleteRecordAsync(
        long id,
        CancellationToken cancellationToken = default)
    {

        try
        {
            return await base.DeleteRecordAsync(id, cancellationToken);
        }
        catch
        {
            return Result<DeleteResponse>
                .Fail(EntityErrorResources.AvatarDeleteFailed);
        }
    }
}
