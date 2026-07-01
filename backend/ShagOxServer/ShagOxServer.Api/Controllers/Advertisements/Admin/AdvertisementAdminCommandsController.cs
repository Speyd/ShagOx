using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Advertisements.Create;
using ShagOxServer.Application.DTOs.Advertisements.Update;
using ShagOxServer.Application.Interfaces.Advertisements.Create;
using ShagOxServer.Application.Interfaces.Advertisements.Delete;
using ShagOxServer.Application.Interfaces.Advertisements.Update;
using ShagOxServer.Application.Common.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Advertisements.Admin;

[ApiController]
[Route("api/admin/advertisements")]
[Authorize(Roles = "Admin")]
public class AdvertisementAdminCommandsController : ControllerBase
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

    [HttpPost]
    public async Task<IActionResult> Add(
        [FromBody] AdvertisementCreateRequest request)
    {
        var result = await _createService.CreateAdvertisementAsync(request);
        return result.ToActionResult();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id)
    {
        var result = await _deleteService.DeleteAdvertisementAsync(id);
        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] AdvertisementUpdateRequest request)
    {
        var result = await _updateService.UpdateAdvertisementAsync(id, request);
        return result.ToActionResult();
    }
}
