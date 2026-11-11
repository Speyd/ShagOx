using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Query;
using ShagOxServer.Domain.Filters.Deliveries;

namespace ShagOxServer.Api.Controllers.Deliveries.DeliveryProviders;
[ApiController]
[Route("api/delivery-providers")]
public class DeliveryProviderQueriesController
    : BaseQueryController<DeliveryProviderDto,
        DeliveryProviderSearchFilter>
{
    public DeliveryProviderQueriesController(
        IDeliveryProviderQueryService queryService,
        IUserContext userContext
    )
        : base(queryService, userContext)
    {
    }
}
