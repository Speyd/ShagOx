using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Base.Requests;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Create;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Update;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Create;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Delete;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Attributes.AttributeDictionaryValues.Update;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Dictionaries.Attributes.AttributeDictionaryValues.Admin;
[ApiController]
[Route("api/admin/attribute-dictionary-values")]
[Authorize(Roles = "Admin")]
public class AttributeDictionaryValueAdminCommandsController
    : ApiController
{
    private readonly IAttributeDictionaryValueCreateService _createService;
    private readonly IAttributeDictionaryValueUpdateService _updateService;
    private readonly IAttributeDictionaryValueDeleteService _deleteService;


    public AttributeDictionaryValueAdminCommandsController(
        IAttributeDictionaryValueCreateService createService,
        IAttributeDictionaryValueUpdateService updateService,
        IAttributeDictionaryValueDeleteService deleteService,
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
        [FromBody] AttributeDictionaryValueCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        [FromRoute] long id,
        [FromBody] AttributeDictionaryValueUpdateRequest request)
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
