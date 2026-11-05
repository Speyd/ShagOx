using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Core.Single;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Base.Pictures;
using ShagOxServer.Domain.Filters;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Pictures.Core;
public partial class PictureQueryRepository<TEntity, TFilter>
    : SearchRepository<TEntity, TFilter>,
      IPictureQueryRepository<TEntity>
    where TEntity : BasePicture
    where TFilter : BaseFilter
{

    public PictureQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<TEntity?> GetByOwnerAsync(
        long ownerId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        var query = ApplyIncludes(_db.Set<TEntity>());

        return await query
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(x => x.OwnerId == ownerId,
                cancellationToken);
    }
}
