using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Create;
using ShagOxServer.Application.DTOs.Specification.Pictures.Create;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Create;
using ShagOxServer.Application.Services.Auth.Users.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Validator;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Avatars.Create;
public class AvatarCreateService
    : IAvatarCreateService
{
    private readonly IRepository<Avatar> _avatarRepository;
    private readonly PictureValidator _pictureValidator;

    private readonly UserValidator _userValidator;


    public AvatarCreateService(
        IRepository<Avatar> avatarRepository,
        PictureValidator pictureValidator,
        UserValidator userValidator)
    {
        _avatarRepository = avatarRepository;
        _pictureValidator = pictureValidator;
        _userValidator = userValidator;
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

        if (!resultLoaderValid.IsSuccess)
            return Result<PictureCreateResponse>.Fail(resultLoaderValid.Error);


        var avatar = AvatarCreater.Create(
            request,
            resultLoaderValid.Value!
        );

        _avatarRepository.Add(avatar);

        return Result<PictureCreateResponse>.Success(
            new PictureCreateResponse(
                avatar.Id,
                avatar.PublicId,
                DateTime.UtcNow
        ));
    }
}