using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Location.Cities.Translations.Create;
using ShagOxServer.Application.DTOs.Location.Cities.Translations.Update;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Delete;
using ShagOxServer.Application.Interfaces.Services.Location.Cities.Translations.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Cities.Translations;

[ApiController]
[Route("api/admin/cities/translations")]
[Authorize(Roles = "Admin")]
public class CityTranslationCommandsController 
    : ApiController
{
    private readonly ICityTranslationCreateService _createService;
    private readonly ICityTranslationUpdateService _updateService;
    private readonly ICityTranslationDeleteService _deleteService;


    public CityTranslationCommandsController(
        ICityTranslationCreateService createService,
        ICityTranslationUpdateService updateService,
        ICityTranslationDeleteService deleteService)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CityTranslationCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] CityTranslationUpdateRequest request)
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