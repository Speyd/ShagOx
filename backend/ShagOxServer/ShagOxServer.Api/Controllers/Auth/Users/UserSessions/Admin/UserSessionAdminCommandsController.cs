using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Management.Trast;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Auth.Users.UserSessions.Admin;

[ApiController]
[Route("api/admin/user-sessions")]
//[Authorize(Roles = "Admin")]
public class UserSessionAdminCommandsController
    : ApiController
{
    private readonly IUserTrastManagementService _trastManagement;


    public UserSessionAdminCommandsController(
        IUserTrastManagementService trastManagement,
        IUserContext userContext
    )
        : base(userContext)
    {
        _trastManagement = trastManagement;
    }


    [HttpPost("{id:long}/revoke")]
    public async Task<IActionResult> RevokeSession(
        [FromRoute] long id,
        CancellationToken cancellationToken)
    {
        var result = await _trastManagement.RevokeSessionByAdminAsync(
            id,
            SessionRevocationReason.AdminRevoked,
            cancellationToken);

        return result.ToActionResult();
    }
}
