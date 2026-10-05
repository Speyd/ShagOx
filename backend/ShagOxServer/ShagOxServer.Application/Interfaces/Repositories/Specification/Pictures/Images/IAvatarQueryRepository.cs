using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.Avatars;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Images;
public interface IAvatarQueryRepository
    : IQueryRepository<Avatar, AvatarSearchFilter>
{
    Task<Avatar?> GetByUserIdAsync(
        long userId);
}