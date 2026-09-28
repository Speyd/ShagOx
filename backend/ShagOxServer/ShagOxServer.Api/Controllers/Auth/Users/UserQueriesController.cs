using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
using ShagOxServer.Domain.Filters.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Auth.Users;

[ApiController]
[Route("api/users")]
public class UserQueriesController 
    : ApiController
{
    private readonly IUserQueryService _queryService;


    public UserQueriesController(
        IUserQueryService queryService)
    {
        _queryService = queryService;
    }


    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(
        [FromRoute] long id)
    {
        var result = await _queryService
            .GetByIdAsync(id);

        return result.ToActionResult();
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMyProfile()
    {
        var result = await _queryService
            .GetMyProfileAsync();

        return result.ToActionResult();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] UserSearchFilter filter,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .SearchAsync(filter, pagination);

        return result.ToActionResult();
    }

    [HttpGet("contact/{contact}")]
    public async Task<IActionResult> GetByContactAsync(
       [FromRoute] string contact)
    {
        var result = await _queryService
            .GetByContactAsync(contact);

        return result.ToActionResult();
    }
}
