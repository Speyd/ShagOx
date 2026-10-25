using ShagOxServer.Domain.Base.Pictures;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Core.Multiple;
public partial interface IPictureOrderQueryRepository<TEntity>
    where TEntity : BasePictureOrder
{
    Task<List<TEntity>> GetByOwnerAsync(
        long ownerId,
        CancellationToken cancellationToken = default);
}
