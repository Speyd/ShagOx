using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Translations.Create;
using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Translations.Update;
using ShagOxServer.Application.DTOs.Location.Regions.Translations.Create;
using ShagOxServer.Application.DTOs.Location.Regions.Translations.Update;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Translations.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Translations.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.ProductTypes.Translations;

[ApiController]
[Route("api/admin/product-types/translations")]
[Authorize(Roles = "Admin")]
public class ProductTypeTranslationCommandsController : ApiController
{
    private readonly IProductTypeTranslationCreateService _createService;
    private readonly IProductTypeTranslationUpdateService _updateService;
    private readonly IProductTypeTranslationDeleteService _deleteService;


    public ProductTypeTranslationCommandsController(
        IProductTypeTranslationCreateService createService,
        IProductTypeTranslationUpdateService updateService,
        IProductTypeTranslationDeleteService deleteService)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        ProductTypeTranslationCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] ProductTypeTranslationUpdateRequest request)
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