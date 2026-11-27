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
    private readonly IOrderExistsRepository _existsRepository;


    public OrderQueriesController(
        IOrderQueryService queryService,
        IOrderExistsRepository existsRepository,
        IUserAdminQueryService userQueryService,
        IUserContext userContext
    )
        : base(existsRepository, userQueryService, userContext)
    {
        _queryService = queryService;
        _existsRepository = existsRepository;
    }


    [HttpGet("me")]
    public async Task<IActionResult> GetMyOrders(
        [FromQuery] OrderSearchFilter filter,
        [FromQuery] PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        var myFilter = filter with 
        {
            SellerId = UserId 
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
        {
            var isSeller = await _existsRepository
                .IsSellerAsync(id, UserId!.Value, cancellationToken);

            if(!isSeller)
                return forbidden;
        }
        
        var result = await _queryService
            .GetByIdAsync(id, cancellationToken);

        return result.ToActionResult();
    }

    [HttpGet("sales")]
    public async Task<IActionResult> GetMySales(
       [FromQuery] OrderSearchFilter filter,
       [FromQuery] PaginationParams pagination,
       CancellationToken cancellationToken)
    {
        var salesFilter = filter with
        {
            SellerId = UserId,
            HideUnpaidOnline = true
        };

        var result = await _queryService
            .SearchAsync(salesFilter, pagination,
                cancellationToken);

        return result.ToActionResult();
    }
}
