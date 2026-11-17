using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Base.Requests;
using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Translations.Create;
using ShagOxServer.Application.DTOs.Deliveries.DeliveryProviders.Translations.Update;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Translations.Delete;
using ShagOxServer.Application.Interfaces.Services.Deliveries.DeliveryProviders.Translations.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Deliveries.DeliveryProviders.Translations.Admin;

[ApiController]
[Route("api/admin/delivery-providers/translations")]
[Authorize(Roles = "Admin")]
public class DeliveryProviderTranslationAdminCommandsController
    : ApiController
{
    private readonly IDeliveryProviderTranslationCreateService _createService;
    private readonly IDeliveryProviderTranslationUpdateService _updateService;
    private readonly IDeliveryProviderTranslationDeleteService _deleteService;


    public DeliveryProviderTranslationAdminCommandsController(
        IDeliveryProviderTranslationCreateService createService,
        IDeliveryProviderTranslationUpdateService updateService,
        IDeliveryProviderTranslationDeleteService deleteService,
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
        DeliveryProviderTranslationCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        [FromRoute] long id,
        [FromBody] DeliveryProviderTranslationUpdateRequest request)
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
