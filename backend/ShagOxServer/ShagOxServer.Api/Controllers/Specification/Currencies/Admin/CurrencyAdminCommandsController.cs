using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Base.Requests;
using ShagOxServer.Application.DTOs.Specification.Currencies.Create;
using ShagOxServer.Application.DTOs.Specification.Currencies.Update;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Update;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Specification.Currencies.Admin;

[ApiController]
[Route("api/admin/currencies")]
[Authorize(Roles = "Admin")]
public class CurrencyAdminCommandsController 
    : ApiController
{
    private readonly ICurrencyCreateService _createService;
    private readonly ICurrencyUpdateService _updateService;
    private readonly ICurrencyDeleteService _deleteService;


    public CurrencyAdminCommandsController(
        ICurrencyCreateService createService,
        ICurrencyUpdateService updateService,
        ICurrencyDeleteService deleteService,
        IUserContext userContext
    )
        : base(userContext)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CurrencyCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        [FromRoute] long id,
        [FromBody] CurrencyUpdateRequest request)
    {
        var result = await _updateService
            .UpdateAsync(id, request);

        return result.ToActionResult();
    }

    [HttpPatch("{id:long}/status")]
    public async Task<IActionResult> ChangeStatus(
        [FromRoute] long id,
        [FromBody] ChangeStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await _updateService
            .ChangeStatusAsync(id, request,
                cancellationToken);

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
