using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Translations.Create;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Translations.Update;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Translations.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Translations.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;


namespace ShagOxServer.Api.Controllers.AttributeDefinitions.Translations;

[ApiController]
[Route("api/admin/attributes/translations")]
[Authorize(Roles = "Admin")]
public class AttributeDefinitionTranslationCommandsController : ApiController
{
    private readonly IAttributeDefinitionTranslationCreateService _createService;
    private readonly IAttributeDefinitionTranslationUpdateService _updateService;
    private readonly IAttributeDefinitionTranslationDeleteService _deleteService;


    public AttributeDefinitionTranslationCommandsController(
        IAttributeDefinitionTranslationCreateService createService,
        IAttributeDefinitionTranslationUpdateService updateService,
        IAttributeDefinitionTranslationDeleteService deleteService)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        AttributeDefinitionTranslationCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] AttributeDefinitionTranslationUpdateRequest request)
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