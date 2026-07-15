using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Advertisements.Update;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Delete;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Advertisements.Admin;

[ApiController]
[Route("api/admin/advertisements")]
[Authorize(Roles = "Admin")]
public class AdvertisementAdminCommandsController : ApiController
{
    private readonly IAdvertisementDeleteService _deleteService;
    private readonly IAdvertisementUpdateService _updateService;

    public AdvertisementAdminCommandsController(
       IAdvertisementDeleteService deleteService,
       IAdvertisementUpdateService updateService)
    {
        _deleteService = deleteService;
        _updateService = updateService;
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id)
    {
        var result = await _deleteService.DeleteAsync(id);
        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] AdvertisementUpdateRequest request)
    {
        var result = await _updateService.UpdateAsync(id, request);
        return result.ToActionResult();
    }
}
