using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Base.Requests;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Update;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Specification.Pictures.Avatars.Admin;

[ApiController]
[Route("api/admin/avatars")]
[Authorize(Roles = "Admin")]
public class AvatarAdminCommandsController
    : ApiController
{
    private readonly IAvatarUpdateService _updateService;


    public AvatarAdminCommandsController(
        IAvatarUpdateService updateService,
        IUserContext userContext
    )
        : base(userContext)
    {
        _updateService = updateService;
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
}
