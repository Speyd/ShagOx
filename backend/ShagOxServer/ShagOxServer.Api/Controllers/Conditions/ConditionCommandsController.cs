using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.SharedKernel.Results.Extensions;
using ShagOxServer.Application.DTOs.Specification.Conditions.Create;
using ShagOxServer.Application.DTOs.Specification.Conditions.Update;
using ShagOxServer.Application.Interfaces.Specification.Conditions.Create;
using ShagOxServer.Application.Interfaces.Specification.Conditions.Delete;
using ShagOxServer.Application.Interfaces.Specification.Conditions.Update;

namespace ShagOxServer.Api.Controllers.Conditions;

[ApiController]
[Route("api/admin/conditions")]
[Authorize(Roles = "Admin")]
public class ConditionCommandsController : ControllerBase
{
    private readonly IConditionCreateService _createService;
    private readonly IConditionUpdateService _updateService;
    private readonly IConditionDeleteService _deleteService;


    public ConditionCommandsController(
        IConditionCreateService createService,
        IConditionUpdateService updateService,
        IConditionDeleteService deleteService)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] ConditionCreateRequest request)
    {
        var result = await _createService.CreateConditionAsync(request);
        return result.ToActionResult();
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] ConditionUpdateRequest request)
    {
        var result = await _updateService.UpdateConditionAsync(id, request);
        return result.ToActionResult();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id)
    {
        var result = await _deleteService.DeleteConditionAsync(id);
        return result.ToActionResult();
    }
}