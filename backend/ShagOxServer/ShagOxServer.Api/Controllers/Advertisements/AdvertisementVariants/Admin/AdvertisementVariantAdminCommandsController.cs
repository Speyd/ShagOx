using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Create;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Update;
using ShagOxServer.Application.DTOs.Base.Requests;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Create;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Delete;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Update;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Advertisements.AdvertisementVariants.Admin;

[ApiController]
[Route("api/admin/advertisement-variants")]
[Authorize(Roles = "Admin")]
public class AdvertisementVariantAdminCommandsController
    : ApiController
{
    private readonly IAdvertisementVariantCreateService _createService;
    private readonly IAdvertisementVariantUpdateService _updateService;
    private readonly IAdvertisementVariantDeleteService _deleteService;


    public AdvertisementVariantAdminCommandsController(
        IAdvertisementVariantCreateService createService,
        IAdvertisementVariantUpdateService updateService,
        IAdvertisementVariantDeleteService deleteService)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        AdvertisementVariantCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        [FromRoute] long id,
        [FromBody] AdvertisementVariantUpdateRequest request)
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
