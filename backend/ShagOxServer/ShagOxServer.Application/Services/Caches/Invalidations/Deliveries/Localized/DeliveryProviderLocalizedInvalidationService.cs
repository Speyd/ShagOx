using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Base.Localized;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements.Localized;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Deliveries;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Deliveries.Localized;
public class DeliveryProviderLocalizedInvalidationService
    : BaseLocalizedCacheInvalidationService<DeliveryProvider>
{
    public DeliveryProviderLocalizedInvalidationService(
        IDataCacheService cache
    )
        : base(cache)
    {
    }
}
