using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Create;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Update;
using ShagOxServer.Application.Interfaces.Dictionaries.AttributeDefinitions.Create;
using ShagOxServer.Application.Interfaces.Dictionaries.AttributeDefinitions.Delete;
using ShagOxServer.Application.Interfaces.Dictionaries.AttributeDefinitions.Update;

namespace ShagOxServer.Api.Controllers.AttributeDefinitions;

[ApiController]
[Route("api/admin/attributes")]
[Authorize(Roles = "Admin")]
public class AttributeDefinitionCommandsController : ControllerBase
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
    public async Task<IActionResult> Create(AttributeDefinitionCreateRequest request)
    {
        var result = await _createService.CreateAttributeDefinitionAsync(request);
        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, AttributeDefinitionUpdateRequest request)
    {
        var result = await _updateService.UpdateAttributeDefinitionAsync(id, request);
        return result.ToActionResult();
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _deleteService.DeleteAttributeDefinitionAsync(id);
        return result.ToActionResult();
    }
}
