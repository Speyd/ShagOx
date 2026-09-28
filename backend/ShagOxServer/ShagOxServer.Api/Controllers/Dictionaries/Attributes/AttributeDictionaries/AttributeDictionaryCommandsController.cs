using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaries.Create;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaries.Update;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaries.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaries.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaries.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Dictionaries.Attributes.AttributeDictionaries;

[ApiController]
[Route("api/admin/attribute-dictionaries")]
[Authorize(Roles = "Admin")]
public class AttributeDictionaryCommandsController
    : ApiController
{
    private readonly IAttributeDictionaryCreateService _createService;
    private readonly IAttributeDictionaryUpdateService _updateService;
    private readonly IAttributeDictionaryDeleteService _deleteService;


    public AttributeDictionaryCommandsController(
        IAttributeDictionaryCreateService createService,
        IAttributeDictionaryUpdateService updateService,
        IAttributeDictionaryDeleteService deleteService
        )
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] AttributeDictionaryCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        [FromRoute] long id,
        [FromBody] AttributeDictionaryUpdateRequest request)
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