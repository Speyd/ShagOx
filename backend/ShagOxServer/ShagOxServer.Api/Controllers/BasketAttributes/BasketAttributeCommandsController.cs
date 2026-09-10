using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Baskets.Create;
using ShagOxServer.Application.DTOs.Baskets.Update;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketAttributes.Create;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketAttributes.Delete;
using ShagOxServer.Application.Interfaces.Services.Baskets.BasketAttributes.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.BasketAttributes;

[ApiController]
[Route("api/admin/basket-attribute")]
[Authorize(Roles = "Admin")]
public class BasketAttributeCommandsController
    : ApiController
{
    private readonly IBasketAttributeCreateService _createService;
    private readonly IBasketAttributeUpdateService _updateService;
    private readonly IBasketAttributeDeleteService _deleteService;


    public BasketAttributeCommandsController(
        IBasketAttributeCreateService createService,
        IBasketAttributeUpdateService updateService,
        IBasketAttributeDeleteService deleteService)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] BasketAttributeCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] BasketAttributeUpdateRequest request)
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