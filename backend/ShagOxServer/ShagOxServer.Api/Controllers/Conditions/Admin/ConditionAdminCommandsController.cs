using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Specification.Conditions.Create;
using ShagOxServer.Application.DTOs.Specification.Conditions.Update;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Conditions.Admin;

[ApiController]
[Route("api/admin/conditions")]
[Authorize(Roles = "Admin")]
public class ConditionAdminCommandsController : ApiController
{
    private readonly IConditionCreateService _createService;
    private readonly IConditionUpdateService _updateService;
    private readonly IConditionDeleteService _deleteService;


    public ConditionAdminCommandsController(
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
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] ConditionUpdateRequest request)
    {
        var result = await _updateService
            .UpdateAsync(id, request);

        return result.ToActionResult();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id)
    {
        var result = await _deleteService
            .DeleteAsync(id);

        return result.ToActionResult();
    }
}