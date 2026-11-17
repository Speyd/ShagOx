using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Base.Special;
using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Enum;

namespace ShagOxServer.Application.Interfaces.Repositories.LineItems.Core;
public interface ILineItemExistsRepository<TEntity>
    : IExistsRepository<TEntity>, 
    IExistsOwnerRepository
    where TEntity : BaseEntity
{
    Task<bool> ExistsAsync(
        long advertisementId,
        long? advertisementVariantId,
        long parentId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<bool> ExistsByParentAsync(
        long itemId,
        long parentId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
