using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Location.Regions.Translations.Create;
using ShagOxServer.Application.DTOs.Location.Regions.Translations.Update;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Translations.Delete;
using ShagOxServer.Application.Interfaces.Services.Location.Regions.Translations.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Regions.Translations;

[ApiController]
[Route("api/admin/regions/translations")]
[Authorize(Roles = "Admin")]
public class RegionTranslationCommandsController 
    : ApiController
{
    private readonly IRegionTranslationCreateService _createService;
    private readonly IRegionTranslationUpdateService _updateService;
    private readonly IRegionTranslationDeleteService _deleteService;


    public RegionTranslationCommandsController(
        IRegionTranslationCreateService createService,
        IRegionTranslationUpdateService updateService,
        IRegionTranslationDeleteService deleteService)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        RegionTranslationCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] RegionTranslationUpdateRequest request)
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