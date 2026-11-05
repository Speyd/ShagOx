using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.Interfaces.Repositories.Base.Special;
public interface IExistsOwnerRepository
{
    Task<bool> IsOwnerAsync(
        long entityId,
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
