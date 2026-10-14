using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Filters.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Auth.Users;

[ApiController]
[Route("api/users")]
public class UserQueriesController 
    : BaseQueryController<UserDto,
        UserSearchFilter>
{
    private readonly IUserQueryService _userQueryService;


    public UserQueriesController(
        IUserQueryService queryService
    )
        : base(queryService)
    {
        _userQueryService = queryService;
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
