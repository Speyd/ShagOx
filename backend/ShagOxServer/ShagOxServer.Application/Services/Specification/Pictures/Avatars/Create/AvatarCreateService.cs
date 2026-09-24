using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Create;
using ShagOxServer.Application.DTOs.Specification.Pictures.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Create;
using ShagOxServer.Application.Resources.EntityErrorResourcess;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Validator;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Avatars.Create;
public class AvatarCreateService
    : IAvatarCreateService
{
    private readonly IRepository<Avatar> _avatarRepository;
    private readonly PictureValidator _pictureValidator;
    private readonly IPictureLoaderService _loaderService;

    private readonly UserValidator _userValidator;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AvatarCreateService> _logger;


    public AvatarCreateService(
        IRepository<Avatar> avatarRepository,
        PictureValidator pictureValidator,
        IPictureLoaderService loaderService,
        UserValidator userValidator,
        IUnitOfWork unitOfWork,
        ILogger<AvatarCreateService> logger)
    {
        _avatarRepository = avatarRepository;
        _pictureValidator = pictureValidator;
        _loaderService = loaderService;
        _userValidator = userValidator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<PictureCreateResponse>> CreateAsync(
        AvatarCreateRequest request)
    {
        var resultAdvertValid = await _userValidator
            .ExistsByIdAsync(request.UserId);

        if (!resultAdvertValid.IsSuccess)
            return Result<PictureCreateResponse>.Fail(resultAdvertValid.Error);

        var resultLoaderValid = await _pictureValidator
            .PictureUploadValidator(request.File);


        await _unitOfWork.BeginTransactionAsync();

        try
        {     
            if (!resultLoaderValid.IsSuccess)
            {
                if (resultLoaderValid.Value is not null)
                {
                    await _loaderService
                        .DeleteAsync(resultLoaderValid.Value.PublicId);
                }

                return Result<PictureCreateResponse>
                    .Fail(resultLoaderValid.Error);
            }


            var avatar = AvatarCreater.Create(
                request,
                resultLoaderValid.Value!
            );


            _avatarRepository.Add(avatar);


            await _unitOfWork.CommitAsync();

            _logger.LogInformation(
                "Avatar created successfully. " + 
                "AvatarId: {AvatarId}, UserId: {UserId}",
                avatar.Id,
                request.UserId);

            return Result<PictureCreateResponse>.Success(
               new PictureCreateResponse(
                   avatar.Id,
                   avatar.PublicId,
                   DateTime.UtcNow
            ));
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            if (resultLoaderValid.Value is not null)
            {
                await _loaderService
                    .DeleteAsync(resultLoaderValid.Value.PublicId);
            }

            _logger.LogError(
               ex,
               "Failed to create avatar. UserId: {UserId}",
               request.UserId);

            return Result<PictureCreateResponse>
                .Fail(EntityErrorResources.AvatarCreateFailed);
        }
    }
}