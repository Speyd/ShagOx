using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Create;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Update;
using ShagOxServer.Application.Interfaces.Dictionaries.AttributeDefinitions.Create;
using ShagOxServer.Application.Interfaces.Dictionaries.AttributeDefinitions.Delete;
using ShagOxServer.Application.Interfaces.Dictionaries.AttributeDefinitions.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.AttributeDefinitions;

[ApiController]
[Route("api/admin/attributes")]
[Authorize(Roles = "Admin")]
public class AttributeDefinitionCommandsController : ApiController
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
        var result = await _createService.CreateAttributeDefinitionAsync(request);
        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] AttributeDefinitionUpdateRequest request)
    {
        var result = await _updateService.UpdateAttributeDefinitionAsync(id, request);
        return result.ToActionResult();
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id)
    {
        var result = await _deleteService.DeleteAttributeDefinitionAsync(id);
        return result.ToActionResult();
    }
}
