using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.DTOs.Specification.Conditions.Create;
using ShagOxServer.Application.DTOs.Specification.Conditions.Update;
using ShagOxServer.Application.Interfaces.Specification.Conditions.Create;
using ShagOxServer.Application.Interfaces.Specification.Conditions.Delete;
using ShagOxServer.Application.Interfaces.Specification.Conditions.Update;

namespace ShagOxServer.Api.Controllers.Conditions;
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
    public async Task<IActionResult> Create(ConditionCreateRequest request)
    {
        var result = await _createService.CreateConditionAsync(request);
        return result.ToActionResult();
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        ConditionUpdateRequest request)
    {
        var result = await _updateService.UpdateConditionAsync(id, request);
        return result.ToActionResult();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _deleteService.DeleteConditionAsync(id);
        return result.ToActionResult();
    }
}