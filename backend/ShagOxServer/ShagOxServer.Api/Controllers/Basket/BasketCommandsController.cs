using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Baskets.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Baskets.Core.Create;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Basket;

[ApiController]
[Route("api/baskets")]
[Authorize]
public class BasketCommandsController
    : ApiController
{
    private readonly IBasketCreateService _createService;


    public BasketCommandsController(
        IBasketCreateService createService)
    {
        _createService = createService;
    }


    [HttpPost]
    public async Task<IActionResult> Create()
    {
        BasketCreateRequest request = new(UserId);

        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }
}