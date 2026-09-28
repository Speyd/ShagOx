using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Create;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Update;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Dictionaries.Attributes.AttributeDictionaryValues.Translations;

[ApiController]
[Route("api/admin/attribute-dictionary-values/translations")]
[Authorize(Roles = "Admin")]
public class AttributeDictionaryValueTranslationCommandsController
    : ApiController
{
    private readonly IAttributeDictionaryValueTranslationCreateService _createService;
    private readonly IAttributeDictionaryValueTranslationUpdateService _updateService;
    private readonly IAttributeDictionaryValueTranslationDeleteService _deleteService;


    public AttributeDictionaryValueTranslationCommandsController(
        IAttributeDictionaryValueTranslationCreateService createService,
        IAttributeDictionaryValueTranslationUpdateService updateService,
        IAttributeDictionaryValueTranslationDeleteService deleteService)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        AttributeDictionaryValueTranslationCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        [FromRoute] long id,
        [FromBody] AttributeDictionaryValueTranslationUpdateRequest request)
    {
        var result = await _updateService
            .UpdateAsync(id, request);

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