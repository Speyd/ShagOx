using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Location.Cities.Create;
using ShagOxServer.Application.DTOs.Location.Cities.Update;
using ShagOxServer.SharedKernel.Results.Extensions;
using ShagOxServer.Application.Interfaces.Location.Cities.Create;
using ShagOxServer.Application.Interfaces.Location.Cities.Delete;
using ShagOxServer.Application.Interfaces.Location.Cities.Update;

namespace ShagOxServer.Api.Controllers.Cities;

[ApiController]
[Route("api/admin/cities")]
[Authorize(Roles = "Admin")]
public class CityCommandsController : ControllerBase
{
    private readonly ICityCreateService _createService;
    private readonly ICityUpdateService _updateService;
    private readonly ICityDeleteService _deleteService;

    public CityCommandsController(
        ICityCreateService createService,
        ICityUpdateService updateService,
        ICityDeleteService deleteService
        )
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CityCreateRequest request)
    {
        var result = await _createService.CreateCityAsync(request);
        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] CityUpdateRequest request)
    {
        var result = await _updateService.UpdateCityAsync(id, request);
        return result.ToActionResult();
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id)
    {
        var result = await _deleteService.DeleteCityAsync(id);
        return result.ToActionResult();
    }
}
