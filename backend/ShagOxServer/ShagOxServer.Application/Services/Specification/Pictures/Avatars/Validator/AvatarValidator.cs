using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Validator;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Specification.Pictures.Avatars.Validator;
public class AvatarValidator
    : BaseValidator<Avatar>
{
    public AvatarValidator(
        IRepository<Avatar> repository,
        IQueryRepository<Avatar> avatarRepository,
        IExistsRepository<Avatar> avatarExistsRepository,
        IDataCacheService cache
    ) 
        : base(repository, avatarRepository, avatarExistsRepository, cache)
    {
    }
}
