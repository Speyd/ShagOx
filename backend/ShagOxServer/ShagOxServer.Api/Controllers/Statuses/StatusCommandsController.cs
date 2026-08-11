using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Advertisements.Statuses.Create;
using ShagOxServer.Application.DTOs.Advertisements.Statuses.Update;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Create;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Delete;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Statuses;

[ApiController]
[Route("api/admin/status")]
[Authorize(Roles = "Admin")]
public class StatusCommandsController : ApiController
{
    private readonly IStatusCreateService _createService;
    private readonly IStatusUpdateService _updateService;
    private readonly IStatusDeleteService _deleteService;


    public StatusCommandsController(
        IStatusCreateService createService,
        IStatusUpdateService updateService,
        IStatusDeleteService deleteService)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        StatusCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] StatusUpdateRequest request)
    {
        var result = await _updateService
            .UpdateAsync(id, request);

        return result.ToActionResult();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id)
    {
        var result = await _deleteService
            .DeleteAsync(id);

        return result.ToActionResult();
    }
}