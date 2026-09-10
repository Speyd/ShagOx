using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Translations.Create;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Translations.Update;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Categories.Translations;

[ApiController]
[Route("api/admin/categories/translations")]
[Authorize(Roles = "Admin")]
public class CategoryTranslationCommandsController 
    : ApiController
{
    private readonly ICategoryTranslationCreateService _createService;
    private readonly ICategoryTranslationUpdateService _updateService;
    private readonly ICategoryTranslationDeleteService _deleteService;


    public CategoryTranslationCommandsController(
        ICategoryTranslationCreateService createService,
        ICategoryTranslationUpdateService updateService,
        ICategoryTranslationDeleteService deleteService)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CategoryTranslationCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] CategoryTranslationUpdateRequest request)
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