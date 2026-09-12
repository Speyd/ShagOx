using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Common;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Query;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.BasketItems;
[ApiController]
[Route("api/basket-items")]
[Authorize]
public class BasketItemQueriesController
    : OwnerExistsController
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


    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .GetPagedAsync(UserId, pagination);

        return result.ToActionResult();
    }
}