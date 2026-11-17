using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Query;
using ShagOxServer.Domain.Filters.Deliveries;

namespace ShagOxServer.Api.Controllers.Deliveries.DeliveryProviders.Admin;

[ApiController]
[Route("api/admin/delivery-providers")]
[Authorize(Roles = "Admin")]
public class DeliveryProviderAdminQueriesController
    : BaseAdminQueryController<DeliveryProviderDto,
        DeliveryProviderSearchFilter>
{
    public DeliveryProviderAdminQueriesController(
        IDeliveryProviderQueryService queryService,
        IUserContext userContext
    )
        : base(queryService, userContext)
    {
    }
}
