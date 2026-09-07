using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Create;
using ShagOxServer.Application.DTOs.Dictionaries.AttributeDefinitions.Update;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.AttributeDefinitions.Update;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.AttributeDefinitions;

[ApiController]
[Route("api/attributes")]
[Authorize]
public class AttributeDefinitionCommandsController : ApiController
{
    private readonly IAttributeDefinitionCreateService _createService;
    private readonly IAttributeDefinitionUpdateService _updateService;
    private readonly IAttributeDefinitionDeleteService _deleteService;
    private readonly ICategoryQueryService _categoryService;


    public AttributeDefinitionCommandsController(
        IAttributeDefinitionCreateService createService,
        IAttributeDefinitionUpdateService updateService,
        IAttributeDefinitionDeleteService deleteService,
        ICategoryQueryService categoryService
        )
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
        _categoryService = categoryService;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] AttributeDefinitionCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] AttributeDefinitionUpdateRequest request)
    {
        var c = await _categoryService.GetByProductTypeAsync(id,  new PaginationParams());

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