using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Deliveries.DeliveryProviders;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Deliveries;
using ShagOxServer.Domain.Filters.Deliveries.DeliveryProviders;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Deliveries.DeliveryProviders.Query;
public partial class DeliveryProviderQueryRepository
    : SearchRepository<DeliveryProvider, 
        DeliveryProviderSearchFilter>,
      IDeliveryProviderQueryRepository
{
    public DeliveryProviderQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<DeliveryProvider?> GetByIdentificatorAsync(
        string identificator,
        long? parentId = null,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.DeliveryProviders
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(x =>
                x.Code == identificator,
                cancellationToken);
    }
}
