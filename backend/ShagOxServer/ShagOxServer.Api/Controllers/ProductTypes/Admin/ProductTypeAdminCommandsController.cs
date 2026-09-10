using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Create;
using ShagOxServer.Application.DTOs.Dictionaries.ProductTypes.Update;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.ProductTypes.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.ProductTypes.Admin;

[ApiController]
[Route("api/admin/product-types")]
[Authorize(Roles = "Admin")]
public class ProductTypeAdminCommandsController 
    : ApiController
{
    private readonly IProductTypeCreateService _createService;
    private readonly IProductTypeUpdateService _updateService;
    private readonly IProductTypeDeleteService _deleteService;


    public ProductTypeAdminCommandsController(
        IProductTypeCreateService createService,
        IProductTypeUpdateService updateService,
        IProductTypeDeleteService deleteService
        )
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] ProductTypeCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] ProductTypeUpdateRequest request)
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