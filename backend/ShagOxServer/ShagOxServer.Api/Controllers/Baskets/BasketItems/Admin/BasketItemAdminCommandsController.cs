using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Base.Requests;
using ShagOxServer.Application.DTOs.Baskets.BasketItems.Update;
using ShagOxServer.Application.DTOs.LineItems.Create;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Create;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Delete;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketItems.Update;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Baskets.BasketItems.Admin;

[ApiController]
[Route("api/admin/basket-items")]
[Authorize(Roles = "Admin")]
public class BasketItemAdminCommandsController
    : ApiController
{
    private readonly IBasketItemCreateService _createService;
    private readonly IBasketItemUpdateService _updateService;
    private readonly IBasketItemDeleteService _deleteService;


    public BasketItemAdminCommandsController(
        IBasketItemCreateService createService,
        IBasketItemUpdateService updateService,
        IBasketItemDeleteService deleteService,
        IUserContext userContext
    )
        : base(userContext)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] LineItemCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        [FromRoute] long id,
        [FromBody] BasketItemUpdateRequest request)
    {
        var result = await _updateService
            .UpdateAsync(id, request);

        return result.ToActionResult();
    }

    [HttpPatch("{id:long}/status")]
    public async Task<IActionResult> ChangeStatus(
        [FromRoute] long id,
        [FromBody] ChangeStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await _updateService
            .ChangeStatusAsync(id, request,
                cancellationToken);

        return result.ToActionResult();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(
        [FromRoute] long id)
    {
        var result = await _deleteService
            .DeleteAsync(id);

        return result.ToActionResult();
    }
}
