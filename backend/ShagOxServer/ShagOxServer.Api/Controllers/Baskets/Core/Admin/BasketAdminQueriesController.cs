using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Baskets.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Baskets.Core.Query;
using ShagOxServer.Domain.Filters.Baskets.Core;

namespace ShagOxServer.Api.Controllers.Baskets.Core.Admin;
[ApiController]
[Route("api/admin/baskets")]
[Authorize(Roles = "Admin")]
public class BasketAdminQueriesController
    : BaseQueryController<BasketDto,
        BasketSearchFilter>
{
    public BasketAdminQueriesController(
        IBasketQueryService queryService
    )
        : base(queryService)
    {
    }
}
