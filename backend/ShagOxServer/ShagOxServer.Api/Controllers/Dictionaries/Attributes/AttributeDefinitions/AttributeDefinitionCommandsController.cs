using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Create;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Update;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDefinitions.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Dictionaries.Attributes.AttributeDefinitions;

[ApiController]
[Route("api/admin/attributes-definitions")]
[Authorize(Roles = "Admin")]
public class AttributeDefinitionCommandsController 
    : ApiController
{
    private readonly IAttributeDefinitionCreateService _createService;
    private readonly IAttributeDefinitionUpdateService _updateService;
    private readonly IAttributeDefinitionDeleteService _deleteService;


    public AttributeDefinitionCommandsController(
        IAttributeDefinitionCreateService createService,
        IAttributeDefinitionUpdateService updateService,
        IAttributeDefinitionDeleteService deleteService
        )
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] AttributeDefinitionCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        [FromRoute] long id,
        [FromBody] AttributeDefinitionUpdateRequest request)
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