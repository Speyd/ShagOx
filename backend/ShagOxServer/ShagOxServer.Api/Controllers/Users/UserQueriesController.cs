using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Users.Query;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;
using ShagOxServer.SharedKernel.Paginations;

namespace ShagOxServer.Api.Controllers.Users;

[ApiController]
[Route("api/users")]
public class UserQueriesController : ControllerBase
{
    private readonly IUserQueryService _queryService;

    public UserQueriesController(
        IUserQueryService queryService)
    {
        _queryService = queryService;
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        [FromRoute] int id)
    {
        var result = await _queryService.GetByIdAsync(id);
        return result.ToActionResult();
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMyProfile()
    {
        var result = await _queryService.GetMyProfileAsync();
        return result.ToActionResult();
    }

    [HttpGet("search/full-name")]
    public async Task<IActionResult> SearchByFullName(
        [FromQuery] string fullName,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService.SearchByFullName(fullName, pagination);
        return result.ToActionResult();
    }

    [HttpGet("search/email")]
    public async Task<IActionResult> SearchByEmail(
        [FromQuery] string email,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService.SearchByEmail(email, pagination);
        return result.ToActionResult();
    }

    [HttpGet("search/phone")]
    public async Task<IActionResult> SearchByPhone(
        [FromQuery] string phone,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService.SearchByPhone(phone, pagination);
        return result.ToActionResult();
    }
}
