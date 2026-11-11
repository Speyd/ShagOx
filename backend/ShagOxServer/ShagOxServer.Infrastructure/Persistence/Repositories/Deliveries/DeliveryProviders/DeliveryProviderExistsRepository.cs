using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Deliveries.DeliveryProviders;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Deliveries;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Deliveries.DeliveryProviders;
public class DeliveryProviderExistsRepository
    : ExistsRepository<DeliveryProvider>,
      IDeliveryProviderExistsRepository
{
    public DeliveryProviderExistsRepository(
        AppDbContext db)
        : base(db)
    { }

    public async Task<bool> ExistsByCodeAsync(
        string code,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Statuses
            .WithStatusFilter(status)
            .AnyAsync(x => x.Code == code,
                cancellationToken);
    }
}
