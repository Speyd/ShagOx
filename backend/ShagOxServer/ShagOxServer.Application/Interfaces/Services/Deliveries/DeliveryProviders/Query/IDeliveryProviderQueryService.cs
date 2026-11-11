using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Query;
using ShagOxServer.Application.Interfaces.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Deliveries;
using ShagOxServer.Domain.Filters.Deliveries;

namespace ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Query;
public interface IDeliveryProviderQueryService
    : ITranslatableQueryService<DeliveryProviderDto,
        DeliveryProvider,
        DeliveryProviderSearchFilter>
{
}
