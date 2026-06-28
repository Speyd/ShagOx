using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Advertisements.Create;
using ShagOxServer.Application.DTOs.Advertisements.Update;
using ShagOxServer.Application.Interfaces.Advertisements.Create;
using ShagOxServer.Application.Interfaces.Advertisements.Delete;
using ShagOxServer.Application.Interfaces.Advertisements.Update;
using ShagOxServer.Application.Common.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Advertisements.Admin;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/admin/advertisements")]
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
    public async Task<IActionResult> Add(AdvertisementCreateRequest request)
    {
        var result = await _createService.CreateAdvertisementAsync(request);
        return result.ToActionResult();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _deleteService.DeleteAdvertisementAsync(id);
        return result.ToActionResult();
    }

    [HttpPut]
    public async Task<IActionResult> Update(AdvertisementUpdateRequest request)
    {
        var result = await _updateService.UpdateAdvertisementAsync(request);
        return result.ToActionResult();
    }
}
