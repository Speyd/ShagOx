using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Base.Requests;
using ShagOxServer.Application.DTOs.Specification.Conditions.Translations.Create;
using ShagOxServer.Application.DTOs.Specification.Conditions.Translations.Update;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Update;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Specification.Conditions.Translations.Admin;

[ApiController]
[Route("api/admin/conditions/translations")]
[Authorize(Roles = "Admin")]
public class ConditionTranslationAdminCommandsController 
    : ApiController
{
    private readonly IConditionTranslationCreateService _createService;
    private readonly IConditionTranslationUpdateService _updateService;
    private readonly IConditionTranslationDeleteService _deleteService;


    public ConditionTranslationAdminCommandsController(
        IConditionTranslationCreateService createService,
        IConditionTranslationUpdateService updateService,
        IConditionTranslationDeleteService deleteService)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        ConditionTranslationCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        [FromRoute] long id,
        [FromBody] ConditionTranslationUpdateRequest request)
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
