using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Baskets.Core.Create;
using ShagOxServer.Application.DTOs.Baskets.Core.Update;
using ShagOxServer.Application.Interfaces.Services.Baskets.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Baskets.Core.Delete;
using ShagOxServer.Application.Interfaces.Services.Baskets.Core.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Baskets.Admin;

[ApiController]
[Route("api/admin/baskets")]
[Authorize(Roles = "Admin")]
public class BasketAdminCommandsController
    : ApiController
{
    private readonly IBasketCreateService _createService;
    private readonly IBasketUpdateService _updateService;
    private readonly IBasketDeleteService _deleteService;


    public BasketAdminCommandsController(
        IBasketCreateService createService,
        IBasketUpdateService updateService,
        IBasketDeleteService deleteService)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] BasketCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] BasketUpdateRequest request)
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