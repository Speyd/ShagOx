using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.SharedKernel.Results.Extensions;
using ShagOxServer.Application.DTOs.Location.Regions.Create;
using ShagOxServer.Application.DTOs.Location.Regions.Update;
using ShagOxServer.Application.Interfaces.Location.Regions.Create;
using ShagOxServer.Application.Interfaces.Location.Regions.Delete;
using ShagOxServer.Application.Interfaces.Location.Regions.Update;

namespace ShagOxServer.Api.Controllers.Regions;

[ApiController]
[Route("api/admin/regions")]
[Authorize(Roles = "Admin")]
public class RegionCommandsController : ControllerBase
{
    private readonly IRegionCreateService _createService;
    private readonly IRegionUpdateService _updateService;
    private readonly IRegionDeleteService _deleteService;

    public RegionCommandsController(
        IRegionCreateService createService,
        IRegionUpdateService updateService,
        IRegionDeleteService deleteService
        )
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] RegionCreateRequest request)
    {
        var result = await _createService.CreateRegionAsync(request);
        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] RegionUpdateRequest request)
    {
        var result = await _updateService.UpdateRegionAsync(id, request);
        return result.ToActionResult();
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id)
    {
        var result = await _deleteService.DeleteRegionAsync(id);
        return result.ToActionResult();
    }
}
