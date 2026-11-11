using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Base.Requests;
using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Create;
using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Update;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Create;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Delete;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Deliveries.DeliveryProviders.Admin;

[ApiController]
[Route("api/admin/delivery-providers")]
[Authorize(Roles = "Admin")]
public class DeliveryProviderAdminCommandsController
    : ApiController
{
    private readonly IDeliveryProviderCreateService _createService;
    private readonly IDeliveryProviderUpdateService _updateService;
    private readonly IDeliveryProviderDeleteService _deleteService;


    public DeliveryProviderAdminCommandsController(
        IDeliveryProviderCreateService createService,
        IDeliveryProviderUpdateService updateService,
        IDeliveryProviderDeleteService deleteService,
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
        DeliveryProviderCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        [FromRoute] long id,
        [FromBody] DeliveryProviderUpdateRequest request)
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
