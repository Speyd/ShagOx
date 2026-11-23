using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Application.DTOs.Auth.Users.Statistics;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Statistics;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Filters.Auth.Users.Core;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Auth.Users;

[ApiController]
[Route("api/users")]
public class UserQueriesController 
    : BaseQueryController<UserDto,
        UserSearchFilter>
{
    private readonly IUserQueryService _userQueryService;
    private readonly IUserStatisticsService _statisticsService;


    public UserQueriesController(
        IUserQueryService queryService,
        IUserStatisticsService statisticsService,
        IUserContext userContext
    )
        : base(queryService, userContext)
    {
        _userQueryService = queryService;
        _statisticsService = statisticsService;
    }


    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMyProfile(
        CancellationToken cancellationToken)
    {
        var result = await _userQueryService
            .GetMyProfileAsync(cancellationToken);

        return result.ToActionResult();
    }

    [Authorize]
    [HttpGet("me/statistics")]
    public async Task<IActionResult> GetMyStatistics(
        [FromQuery] UserStatisticQueryOptions options,
        CancellationToken cancellationToken)
    {
        var result = await _statisticsService
            .GetByUserAsync(
                UserId!.Value, 
                options,
                cancellationToken);

        return result.ToActionResult();
    }


    [HttpGet("contact/{contact}")]
    public async Task<IActionResult> GetByContactAsync(
       [FromRoute] string contact,
        CancellationToken cancellationToken)
    {
        var result = await _userQueryService
            .GetByContactAsync(contact,
                cancellationToken);

        return result.ToActionResult();
    }
}
