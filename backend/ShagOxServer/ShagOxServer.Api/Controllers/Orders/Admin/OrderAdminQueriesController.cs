using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Orders.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Orders.Core.Query;
using ShagOxServer.Domain.Filters.Orders.Core;

namespace ShagOxServer.Api.Controllers.Orders.Admin;

[ApiController]
[Route("api/admin/orders")]
[Authorize(Roles = "Admin")]
public class OrderAdminQueriesController
    : BaseAdminQueryController<OrderDto,
        OrderSearchFilter>
{
    public OrderAdminQueriesController(
        IOrderQueryService queryService,
        IUserContext userContext
    )
        : base(queryService, userContext)
    {
    }
}
