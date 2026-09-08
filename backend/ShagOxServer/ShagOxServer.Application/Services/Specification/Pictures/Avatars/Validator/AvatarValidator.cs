using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Specification.Pictures.Avatars.Validator;
public class AvatarValidator
    : BaseValidator<Avatar>
{
    public AvatarValidator(
        IRepository<Avatar> avatarRepository,
        IExistsRepository<Avatar> avatarExistsRepository
    ) : base(avatarRepository, avatarExistsRepository)
    {
    }
}