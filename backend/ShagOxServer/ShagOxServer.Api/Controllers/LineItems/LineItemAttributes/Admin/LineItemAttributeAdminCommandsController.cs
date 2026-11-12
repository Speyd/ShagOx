using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Base.Requests;
using ShagOxServer.Application.DTOs.LineItems.LineItemAttributes.Create;
using ShagOxServer.Application.DTOs.LineItems.LineItemAttributes.Update;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.LineItems.BasketAttributes.Create;
using ShagOxServer.Application.Interfaces.Services.LineItems.BasketAttributes.Delete;
using ShagOxServer.Application.Interfaces.Services.LineItems.BasketAttributes.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.LineItems.LineItemAttributes.Admin;

[ApiController]
[Route("api/admin/line-item-attributes")]
[Authorize(Roles = "Admin")]
public class LineItemAttributeAdminCommandsController
    : ApiController
{
    private readonly ILineItemAttributeCreateService _createService;
    private readonly ILineItemAttributeUpdateService _updateService;
    private readonly ILineItemAttributeDeleteService _deleteService;


    public LineItemAttributeAdminCommandsController(
        ILineItemAttributeCreateService createService,
        ILineItemAttributeUpdateService updateService,
        ILineItemAttributeDeleteService deleteService,
        IUserContext userContext
    )
        : base(userContext)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] LineItemAttributeCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        [FromRoute] long id,
        [FromBody] LineItemAttributeUpdateRequest request)
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
