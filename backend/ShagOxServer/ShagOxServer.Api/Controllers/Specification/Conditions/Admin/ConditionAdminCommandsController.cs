using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Base.Requests;
using ShagOxServer.Application.DTOs.Specification.Conditions.Create;
using ShagOxServer.Application.DTOs.Specification.Conditions.Update;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Update;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Specification.Conditions.Admin;

[ApiController]
[Route("api/admin/conditions")]
[Authorize(Roles = "Admin")]
public class ConditionAdminCommandsController 
    : ApiController
{
    private readonly IConditionCreateService _createService;
    private readonly IConditionUpdateService _updateService;
    private readonly IConditionDeleteService _deleteService;


    public ConditionAdminCommandsController(
        IConditionCreateService createService,
        IConditionUpdateService updateService,
        IConditionDeleteService deleteService,
        IUserContext userContext
    )
        : base(userContext)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] ConditionCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        [FromRoute] long id,
        [FromBody] ConditionUpdateRequest request)
    {
        var result = await _updateService
            .UpdateAsync(id, request);

        return result.ToActionResult();
    }

    [HttpPatch("{id:long}/status")]
    public async Task<IActionResult> ChangeStatus(
        [FromRoute] long id,
        [FromBody] ChangeStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await _updateService
            .ChangeStatusAsync(id, request,
                cancellationToken);

        return result.ToActionResult();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(
        [FromRoute] long id)
    {
        var result = await _deleteService
            .DeleteAsync(id);

        return result.ToActionResult();
    }
}
