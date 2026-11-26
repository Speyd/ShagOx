using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Auth.Users.UserSessions;
[ApiController]
[Route("api/user-sessions")]
[Authorize]
public class UserSessionQueriesController
     : ApiController
{
    private readonly IUserSessionQueryService _queryService;


    public UserSessionQueriesController(
        IUserSessionQueryService queryService,
        IUserContext userContext
    )
        : base(userContext)
    {
        _queryService = queryService;
    }


    [HttpGet("me")]
    public async Task<IActionResult> GetMe(
        [FromQuery] PaginationParams pagination,
        CancellationToken cancellationToken)
    {
        if (!UserId.HasValue)
            return Unauthorized();

        var result = await _queryService.GetByUserAsync(
            UserId.Value, pagination, cancellationToken);

        return result.ToActionResult();
    }

}
