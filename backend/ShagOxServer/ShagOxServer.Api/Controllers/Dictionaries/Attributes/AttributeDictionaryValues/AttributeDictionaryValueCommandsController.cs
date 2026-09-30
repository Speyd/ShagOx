using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Create;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Update;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Dictionaries.Attributes.AttributeDictionaryValues;
[ApiController]
[Route("api/admin/attribute-dictionary-values")]
[Authorize(Roles = "Admin")]
public class AttributeDictionaryValueCommandsController
    : ApiController
{
    private readonly IAttributeDictionaryValueCreateService _createService;
    private readonly IAttributeDictionaryValueUpdateService _updateService;
    private readonly IAttributeDictionaryValueDeleteService _deleteService;


    public AttributeDictionaryValueCommandsController(
        IAttributeDictionaryValueCreateService createService,
        IAttributeDictionaryValueUpdateService updateService,
        IAttributeDictionaryValueDeleteService deleteService
        )
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] AttributeDictionaryValueCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        [FromRoute] long id,
        [FromBody] AttributeDictionaryValueUpdateRequest request)
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