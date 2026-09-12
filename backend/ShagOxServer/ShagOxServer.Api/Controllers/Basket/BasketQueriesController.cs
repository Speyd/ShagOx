using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Services.Baskets.Core.Query;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Basket;
[ApiController]
[Route("api/baskets")]
[Authorize]
public class BasketQueriesController
    : ApiController
{
    private readonly IBasketQueryService _queryService;


    public BasketQueriesController(
        IBasketQueryService queryService)
    {
        _queryService = queryService;
    }


    [HttpGet]
    public async Task<IActionResult> GetById(
        [FromRoute] int id)
    {
        var result = await _queryService
            .GetByUserAsync(UserId);

        return result.ToActionResult();
    }
}