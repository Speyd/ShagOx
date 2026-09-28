using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Advertisements.Core.Create;
using ShagOxServer.Application.DTOs.Advertisements.Core.Update;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Delete;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Advertisements.Core.Admin;

[ApiController]
[Route("api/admin/advertisements")]
[Authorize(Roles = "Admin")]
public class AdvertisementAdminCommandsController 
    : ApiController
{
    private readonly IAdvertisementCreateService _createService;
    private readonly IAdvertisementDeleteService _deleteService;
    private readonly IAdvertisementUpdateService _updateService;


    public AdvertisementAdminCommandsController(
        IAdvertisementCreateService createService,
        IAdvertisementDeleteService deleteService,
        IAdvertisementUpdateService updateService)
    {
        _createService = createService;
        _deleteService = deleteService;
        _updateService = updateService;
    }

    [HttpPost("{userId:long}")]
    public async Task<IActionResult> Create(
        [FromRoute] long userId,
        [FromForm] AdvertisementCreateRequest request)
    {
        foreach (var formField in Request.Form)
        {
            Console.WriteLine(
                $"FORM: {formField.Key} = >>>{formField.Value}<<<");
        }

        var result = await _createService
            .CreateAsync(request, userId);

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

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        [FromRoute] long id,
        [FromForm] AdvertisementUpdateRequest request)
    {
        var result = await _updateService
            .UpdateAsync(id, request);

        return result.ToActionResult();
    }
}