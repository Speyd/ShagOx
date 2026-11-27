using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Filters.Auth.Users.UserDevices;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Auth.Users.UserDevices.Admin;

[ApiController]
[Route("api/admin/user-devices")]
[Authorize(Roles = "Admin")]
public class UserDeviceAdminQueriesController
    : BaseAdminQueryController<UserDeviceDto,
        UserDeviceSearchFilter>
{
    private readonly IUserDeviceQueryService _deviceQueryService;


    public UserDeviceAdminQueriesController(
        IUserDeviceQueryService deviceQueryService,
        IUserContext userContext
    )
        : base(deviceQueryService, userContext)
    {
        _deviceQueryService = deviceQueryService;
    }


    [HttpGet("user/{userId:long}/device/{deviceId:long}")]
    public async Task<IActionResult> GetByUserAndDevice(
        [FromRoute] long userId,
        [FromRoute] long deviceId,
        [FromQuery] EntityStatus status,
        CancellationToken cancellationToken)
    {
        var result = await _deviceQueryService
            .GetByUserAndDeviceAsync(userId, deviceId,
                cancellationToken, status);

        return result.ToActionResult();
    }

    [HttpGet("user/{userId:long}")]
    public async Task<IActionResult> GetByUser(
        [FromRoute] long userId,
        [FromQuery] PaginationParams pagination,
        [FromQuery] EntityStatus status,
        CancellationToken cancellationToken)
    {
        var result = await _deviceQueryService
            .GetByUserAsync(userId, pagination,
                cancellationToken, status);

        return result.ToActionResult();
    }

    [HttpGet("device/{deviceId:long}")]
    public async Task<IActionResult> GetByDevice(
        [FromRoute] long deviceId,
        [FromQuery] PaginationParams pagination,
        [FromQuery] EntityStatus status,
        CancellationToken cancellationToken)
    {
        var result = await _deviceQueryService
            .GetByDeviceAsync(deviceId, pagination,
                cancellationToken, status);

        return result.ToActionResult();
    }
}
