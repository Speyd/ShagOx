using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Advertisements.Statuses.Translations.Create;
using ShagOxServer.Application.DTOs.Advertisements.Statuses.Translations.Update;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Translations.Delete;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Translations.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Statuses.Translations;

[ApiController]
[Route("api/admin/statuses/translations")]
[Authorize(Roles = "Admin")]
public class StatusTranslationCommandsController 
    : ApiController
{
    private readonly IStatusTranslationCreateService _createService;
    private readonly IStatusTranslationUpdateService _updateService;
    private readonly IStatusTranslationDeleteService _deleteService;


    public StatusTranslationCommandsController(
        IStatusTranslationCreateService createService,
        IStatusTranslationUpdateService updateService,
        IStatusTranslationDeleteService deleteService)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        StatusTranslationCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] StatusTranslationUpdateRequest request)
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