using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Location.Cities.Create;
using ShagOxServer.Application.DTOs.Location.Cities.Update;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Create;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Delete;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Cities;

[ApiController]
[Route("api/admin/cities")]
[Authorize(Roles = "Admin")]
public class CityCommandsController : ApiController
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
        var result = await _createService.CreateAsync(request);
        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] CityUpdateRequest request)
    {
        var result = await _updateService.UpdateAsync(id, request);
        return result.ToActionResult();
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id)
    {
        var result = await _deleteService.DeleteAsync(id);
        return result.ToActionResult();
    }
}