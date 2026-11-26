using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Management;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Auth.Users.UserSessions;

[ApiController]
[Route("api/user-sessions")]
[Authorize]
public class UserSessionCommandsController
    : ApiController
{
    private readonly IUserTrastManagementService _trastManagement;


    public UserSessionCommandsController(
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
        var result = await _trastManagement.RevokeSessionAsync(
            id, 
            SessionRevocationReason.UserRevoked,
            cancellationToken);

        return result.ToActionResult();
    }
}
