using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Roles.Update;
using ShagOxServer.Application.Interfaces.Roles.Create;
using ShagOxServer.Application.Interfaces.Roles.Delete;
using ShagOxServer.Application.Interfaces.Roles.Update;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.DTOs.Roles.Create;

namespace ShagOxServer.Api.Controllers.Role;

[ApiController]
[Route("api/admin/role")]
[Authorize(Roles = "Admin")]
public class RoleCommandsController : ControllerBase
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

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(RoleCreateRequest request)
    {
        var result = await _createService.CreateRoleAsync(request);
        return result.ToActionResult();
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id, 
        RoleUpdateRequest request)
    {
        var result = await _updateService.UpdateRoleAsync(id, request);
        return result.ToActionResult();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _deleteService.DeleteRoleAsync(id);
        return result.ToActionResult();
    }
}
