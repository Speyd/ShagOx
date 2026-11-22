using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Interfaces.Repositories.Orders.Core;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Orders.Core.Query;
using ShagOxServer.Domain.Filters.Orders.Core;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Orders;
[ApiController]
[Route("api/orders")]
[Authorize]
public class OrderQueriesController
    : ApiOwnerExistsController
{
    private readonly IOrderQueryService _queryService;


    public OrderQueriesController(
        IOrderQueryService queryService,
        IOrderExistsRepository orderExistsRepository,
        IUserAdminQueryService userQueryService,
        IUserContext userContext
    )
        : base(orderExistsRepository, userQueryService, userContext)
    {
        _queryService = queryService;
    }


    [HttpGet("me")]
    public async Task<IActionResult> GetMyOrders(
        [FromQuery] OrderSearchFilter filter,
        [FromQuery] PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        var myFilter = filter with 
        {
            OwnerId = UserId 
        };


        var result = await _queryService
            .SearchAsync(myFilter, pagination,
                cancellationToken);

        return result.ToActionResult();
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(
        [FromRoute] long id,
        CancellationToken cancellationToken)
    {
        var forbidden = await CheckOwnershipAsync(id);
        if (forbidden is not null)
            return forbidden;

        var result = await _queryService
            .GetByIdAsync(id, cancellationToken);

        return result.ToActionResult();
    }
}
