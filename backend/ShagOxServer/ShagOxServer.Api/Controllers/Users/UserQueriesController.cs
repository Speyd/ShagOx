using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Users.Query;
using ShagOxServer.Application.Interfaces.Users.Update;
using ShagOxServer.Application.Common.Results.Extensions;

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
    public async Task<IActionResult> GetById(int id)
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
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await _queryService.SearchByFullName(fullName, page, pageSize);
        return result.ToActionResult();
    }

    [HttpGet("search/email")]
    public async Task<IActionResult> SearchByEmail(
        string email,
        int page,
        int pageSize)
    {
        var result = await _queryService.SearchByEmail(email, page, pageSize);
        return result.ToActionResult();
    }

    [HttpGet("search/phone")]
    public async Task<IActionResult> SearchByPhone(
        string phone,
        int page,
        int pageSize)
    {
        var result = await _queryService.SearchByPhone(phone, page, pageSize);
        return result.ToActionResult();
    }
}
