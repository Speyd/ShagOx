using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Auth.Roles.Create;
using ShagOxServer.Application.DTOs.Auth.Roles.Update;
using ShagOxServer.Application.DTOs.Base.Requests;
using ShagOxServer.Application.Interfaces.Services.Auth.Roles.Create;
using ShagOxServer.Application.Interfaces.Services.Auth.Roles.Delete;
using ShagOxServer.Application.Interfaces.Services.Auth.Roles.Update;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Auth.Roles;

[ApiController]
[Route("api/admin/roles")]
[Authorize(Roles = "Admin")]
public class RoleCommandsController 
    : ApiController
{
    private readonly IRoleCreateService _createService;
    private readonly IRoleUpdateService _updateService;
    private readonly IRoleDeleteService _deleteService;


    public RoleCommandsController(
        IRoleCreateService createService,
        IRoleUpdateService updateService,
        IRoleDeleteService deleteService)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(RoleCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        [FromRoute] long id,
        [FromBody] RoleUpdateRequest request)
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
