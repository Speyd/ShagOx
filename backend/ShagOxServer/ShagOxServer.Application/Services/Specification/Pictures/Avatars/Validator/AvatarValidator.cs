using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Avatars.Validator;
public class AvatarValidator
{
    private readonly IRepository<Avatar> _avatarRepository;


    public AvatarValidator(
        IRepository<Avatar> avatarRepository)
    {
        _avatarRepository = avatarRepository;
    }


    public async Task<Result<Avatar>> GetByIdAsync(
        int avatarid)
    {
        var avatar = await _avatarRepository
            .GetByIdAsync(avatarid);

        if (avatar is null)
            return Result<Image>.NotFound("Avatar");

        return Result<Image>.Success(avatar);
    }
}
