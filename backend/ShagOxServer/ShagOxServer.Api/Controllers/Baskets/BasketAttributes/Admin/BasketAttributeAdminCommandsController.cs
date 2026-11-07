using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Base.Requests;
using ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Create;
using ShagOxServer.Application.DTOs.Baskets.BasketAttributes.Update;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketAttributes.Create;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketAttributes.Delete;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketAttributes.Update;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Baskets.BasketAttributes.Admin;

[ApiController]
[Route("api/admin/basket-attributes")]
[Authorize(Roles = "Admin")]
public class BasketAttributeAdminCommandsController
    : ApiController
{
    private readonly IBasketAttributeCreateService _createService;
    private readonly IBasketAttributeUpdateService _updateService;
    private readonly IBasketAttributeDeleteService _deleteService;


    public BasketAttributeAdminCommandsController(
        IBasketAttributeCreateService createService,
        IBasketAttributeUpdateService updateService,
        IBasketAttributeDeleteService deleteService,
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
        [FromBody] BasketAttributeCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        [FromRoute] long id,
        [FromBody] BasketAttributeUpdateRequest request)
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
