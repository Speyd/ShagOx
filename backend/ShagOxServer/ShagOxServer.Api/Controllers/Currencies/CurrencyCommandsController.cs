using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.DTOs.Specification.Currencies.Create;
using ShagOxServer.Application.DTOs.Specification.Currencies.Update;
using ShagOxServer.Application.Interfaces.Specification.Currencies.Create;
using ShagOxServer.Application.Interfaces.Specification.Currencies.Delete;
using ShagOxServer.Application.Interfaces.Specification.Currencies.Update;

namespace ShagOxServer.Api.Controllers.Currencies;
public class CurrencyCommandsController : ControllerBase
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
        var result = await _createService.CreateCurrencyAsync(request);
        return result.ToActionResult();
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        CurrencyUpdateRequest request)
    {
        var result = await _updateService.UpdateCurrencyAsync(id, request);
        return result.ToActionResult();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _deleteService.DeleteCurrencyAsync(id);
        return result.ToActionResult();
    }
}
