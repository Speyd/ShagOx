using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Common;
using ShagOxServer.Application.DTOs.Baskets.BasketItems.Create;
using ShagOxServer.Application.DTOs.Baskets.BasketItems.Update;
using ShagOxServer.Application.Interfaces.Repositories.Baskets.Core;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Query;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Create;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Delete;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.BasketItems;
[ApiController]
[Route("api/basket-items")]
[Authorize]
public class BasketItemCommandsController
    : OwnerExistsController
{
    private readonly IBasketItemCreateService _createService;
    private readonly IBasketItemUpdateService _updateService;
    private readonly IBasketItemDeleteService _deleteService;

    protected readonly IBasketExistsRepository _basketExistsService;


    public BasketItemCommandsController(
        IBasketItemCreateService createService,
        IBasketItemUpdateService updateService,
        IBasketItemDeleteService deleteService,
        IBasketExistsRepository basketExistsService,
        IUserAdminQueryService userQueryService
    ) : base(basketExistsService, userQueryService)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
        _basketExistsService = basketExistsService;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] BasketItemCreateRequest request)
    {
        var forbidden = await CheckAdvertisementOwnerAsync(request.BasketId);
        if (forbidden is not null)
            return forbidden;

        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] BasketItemUpdateRequest request)
    {
        var forbidden = await CheckAdvertisementOwnerAsync(id);
        if (forbidden is not null)
            return forbidden;

        var result = await _updateService
            .UpdateAsync(id, request);

        return result.ToActionResult();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id)
    {
        var forbidden = await CheckAdvertisementOwnerAsync(id);
        if (forbidden is not null)
            return forbidden;

        var result = await _deleteService
            .DeleteAsync(id);

        return result.ToActionResult();
    }
}