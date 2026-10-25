using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Baskets.BasketItems;
[ApiController]
[Route("api/basket-items")]
//[Authorize]
public class BasketItemQueriesController
    : ApiOwnerExistsController
{
    private readonly IBasketItemQueryService _queryService;


    public BasketItemQueriesController(
        IBasketItemQueryService queryService,
        IBasketExistsRepository basketExistsService,
        IUserAdminQueryService userQueryService
    ) : base(basketExistsService, userQueryService)
    {
        _queryService = queryService;
    }


    [HttpGet("{id:long}")]
    public virtual async Task<IActionResult> GetById(
      [FromRoute] long id,
       CancellationToken cancellationToken)
    {
        if (CheckOwnershipAsync(id) is not null)
            return Forbid();

        var result = await _queryService
            .GetByIdAsync(id, cancellationToken);

        return result.ToActionResult();
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery] PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        var result = await _queryService
            .GetPagedAsync(UserId, pagination,
                cancellationToken);

        return result.ToActionResult();
    }
}
