using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Core.Multiple;
using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.Domain.Filters;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Core.Multiple.Query;
public partial class PictureOrderQueryRepository<TEntity, TFilter>
    : SearchRepository<TEntity, TFilter>,
      IPictureOrderQueryRepository<TEntity>
    where TEntity : BasePictureOrder
    where TFilter : BaseFilter
{
    public PictureOrderQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<List<TEntity>> GetByOwnerAsync(
        long ownerId,
        CancellationToken cancellationToken = default)
    {
        var query = ApplyIncludes(_db.Set<TEntity>());

        return await query
          .Where(x => x.OwnerId == ownerId)
          .ToListAsync(cancellationToken);
    }
}
