using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Specification.Conditions.Translations.Create;
using ShagOxServer.Application.DTOs.Specification.Conditions.Translations.Update;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Conditions.Translations;

[ApiController]
[Route("api/admin/conditions/translations")]
[Authorize(Roles = "Admin")]
public class ConditionTranslationCommandsController 
    : ApiController
{
    private readonly IConditionTranslationCreateService _createService;
    private readonly IConditionTranslationUpdateService _updateService;
    private readonly IConditionTranslationDeleteService _deleteService;


    public ConditionTranslationCommandsController(
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

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] ConditionTranslationUpdateRequest request)
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