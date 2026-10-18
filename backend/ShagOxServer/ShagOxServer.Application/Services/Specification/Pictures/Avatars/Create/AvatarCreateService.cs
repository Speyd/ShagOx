using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Response;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
using ShagOxServer.Application.Services.Specification.Pictures.Avatars.Mapping;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Create;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Avatars.Create;
public partial class AvatarCreateService
    : PictureCreateService<User,
        Avatar,
        PictureCreateRequest>,
    IAvatarCreateService
{
    private protected AvatarInvalidationService _avatarInvalidation;

    public AvatarCreateService(
        IRepository<Avatar> imageRepository,
        PictureValidator pictureValidator,
        UserValidator advertValidator,
        AvatarInvalidationService avatarInvalidation,
        IUnitOfWork unitOfWork,
        ILogger<AvatarCreateService> logger
    )
        : base(imageRepository,
            pictureValidator,
            advertValidator,
            unitOfWork,
            logger)
    {
        _avatarInvalidation = avatarInvalidation;
    }


    public override async Task<Result<PictureCreateResponse>> CreateAsync(
        PictureCreateRequest request)
    {
        try
        {
            return await base.CreateAsync(request);
        }
        catch
        {
            return Result<PictureCreateResponse>.Fail(
                EntityErrorResources.AvatarCreateFailed);
        }
    }

    public override async Task<Result<PictureCreateResponse>> CreateInternalAsync(
       PictureCreateRequest request)
    {
        try
        {
            return await base.CreateInternalAsync(request);
        }
        catch
        {
            return Result<PictureCreateResponse>.Fail(
                EntityErrorResources.AvatarCreateFailed);
        }
    }

    protected override Avatar CreatePicture(
        PictureCreateRequest request,
        PictureLoaderUploadResponse response)
    {
        return AvatarCreater
            .Create(request, response);
    }
    public override async Task InvalidateCache(
        Avatar avatar)
    {
        await _avatarInvalidation.InvalidateCreateAsync(
            AvatarCacheMapper.ToInfo(avatar));
    }
}
