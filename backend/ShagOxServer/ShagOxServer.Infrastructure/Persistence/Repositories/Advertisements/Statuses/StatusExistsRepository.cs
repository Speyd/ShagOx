using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Advertisements.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses;
public class StatusExistsRepository
    : ExistsRepository<Status>,
      IStatusExistsRepository
{
    public StatusExistsRepository(
        AppDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }

    public async Task<bool> ExistsByCodeAsync(
        AdvertisementStatus code,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Statuses
            .WithStatusFilter(status)
            .AnyAsync(x => x.Code == code,
                cancellationToken);
    }
}
