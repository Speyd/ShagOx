using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses.Query;
public partial class StatusQueryRepository
    : SearchRepository<Status, StatusSearchFilter>,
      IStatusQueryRepository
{
    public StatusQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<Status?> GetByIdentificatorAsync(
        string identificator,
        long? parentId = null)
    {
        return await _db.Statuses
            .FirstOrDefaultAsync(x =>
                x.Code == identificator);
    }
}
