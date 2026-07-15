using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Specification.Currencies.Create;
using ShagOxServer.Application.DTOs.Specification.Currencies.Update;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Currencies;

[ApiController]
[Route("api/admin/currencies")]
[Authorize(Roles = "Admin")]
public class CurrencyCommandsController : ApiController
{
    private readonly ICurrencyCreateService _createService;
    private readonly ICurrencyUpdateService _updateService;
    private readonly ICurrencyDeleteService _deleteService;


    public CurrencyCommandsController(
        ICurrencyCreateService createService,
        ICurrencyUpdateService updateService,
        ICurrencyDeleteService deleteService)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CurrencyCreateRequest request)
    {
        var result = await _createService.CreateAsync(request);
        return result.ToActionResult();
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] CurrencyUpdateRequest request)
    {
        var result = await _updateService.UpdateAsync(id, request);
        return result.ToActionResult();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id)
    {
        var result = await _deleteService.DeleteAsync(id);
        return result.ToActionResult();
    }
}
