using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Base.Requests;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Create;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Update;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Update;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Dictionaries.Categories.Admin;

[ApiController]
[Route("api/admin/categories")]
[Authorize(Roles = "Admin")]
public class CategoryAdminCommandsController 
    : ApiController
{
    private readonly ICategoryCreateService _createService;
    private readonly ICategoryUpdateService _updateService;
    private readonly ICategoryDeleteService _deleteService;


    public CategoryAdminCommandsController(
        ICategoryCreateService createService,
        ICategoryUpdateService updateService,
        ICategoryDeleteService deleteService,
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
        [FromBody] CategoryCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        [FromRoute] long id,
        [FromBody] CategoryUpdateRequest request)
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
