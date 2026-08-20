using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Images;
public interface IAvatarQueryRepository
    : IQueryRepository<Avatar>
{
    Task<Avatar?> GetByUserIdAsync(int advertId);
}