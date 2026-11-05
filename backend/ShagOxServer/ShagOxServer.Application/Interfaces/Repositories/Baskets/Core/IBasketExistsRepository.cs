using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Special;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
public interface IBasketExistsRepository
    : IExistsRepository<Basket>, IExistsOwnerRepository
{
    Task<bool> ExistsByUserAsync(
        long userId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
