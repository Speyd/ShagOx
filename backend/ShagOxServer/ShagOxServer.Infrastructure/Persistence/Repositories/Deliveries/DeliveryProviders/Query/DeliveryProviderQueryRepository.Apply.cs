using ShagOxServer.Application.Interfaces.Repositories.Deliveries.DeliveryProviders;
using ShagOxServer.Domain.Entities.Deliveries;
using ShagOxServer.Domain.Filters.Deliveries.DeliveryProviders;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Deliveries.DeliveryProviders.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Deliveries.DeliveryProviders.Query;
public partial class DeliveryProviderQueryRepository
    : SearchRepository<DeliveryProvider,
        DeliveryProviderSearchFilter>,
      IDeliveryProviderQueryRepository
{
    public override IQueryable<DeliveryProvider> ApplyFilter(
       IQueryable<DeliveryProvider> query,
       DeliveryProviderSearchFilter filter)
    {
        return query.Filter(filter);
    }
}
