using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Auth.Users.UserSessions.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Filters.Auth.Users.UserSessions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Auth.Users.UserSessions.Admin;

[ApiController]
[Route("api/admin/user-sessions")]
//[Authorize(Roles = "Admin")]
public class UserSessionAdminQueriesController
    : BaseQueryController<UserSessionDto,
        UserSessionSearchFilter>
{
    private readonly IUserSessionQueryService _sessionQueryService;


    public UserSessionAdminQueriesController(
        IUserSessionQueryService queryService,
        IUserContext userContext
    )
        : base(queryService, userContext)
    {
        _sessionQueryService = queryService;
    }


    [HttpGet("user/{userId:long}")]
    public async Task<IActionResult> GetByUser(
        [FromRoute] long userId,
        [FromQuery] PaginationParams pagination,
        [FromQuery] EntityStatus status,
        CancellationToken cancellationToken)
    {
        var result = await _sessionQueryService
            .GetByUserAsync(userId, pagination,
                cancellationToken, status);

        return result.ToActionResult();
    }

    [HttpGet("user-devices/{deviceId:long}")]
    public async Task<IActionResult> GetByUserDevice(
        [FromRoute] long deviceId,
        [FromQuery] PaginationParams pagination,
        [FromQuery] EntityStatus status,
        CancellationToken cancellationToken)
    {
        var result = await _sessionQueryService
           .GetByUserDeviceAsync(deviceId, pagination,
               cancellationToken, status);

        return result.ToActionResult();
    }
}
