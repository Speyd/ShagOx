using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Services.Users.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Users.Admin;

[ApiController]
[Route("api/admin/users")]
[Authorize(Roles = "Admin")]
public class UserAdminQueriesController : ApiController
{
    private readonly IUserAdminQueryService _queryService;


    public UserAdminQueriesController(
        IUserAdminQueryService queryService)
    {
        _queryService = queryService;
    }



    [HttpGet("by-contact")]
    public async Task<IActionResult> GetByContact(
        [FromQuery] string? email,
        [FromQuery] string? phone)
    {
        var result = await _queryService.GetByContactAsync(email, phone);
        return result.ToActionResult();
    }

    [HttpGet("by-city/{cityId:int}")]
    public async Task<IActionResult> GetByCity(
        [FromRoute] int cityId,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .GetByCityAsync(cityId, pagination);

        return result.ToActionResult();
    }

    [HttpGet("registered-after")]
    public async Task<IActionResult> GetRegisteredAfter(
        [FromQuery] DateTime date,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .GetUsersRegisteredAfterAsync(date, pagination);

        return result.ToActionResult();
    }

    [HttpGet("active-after")]
    public async Task<IActionResult> GetActiveAfter(
        [FromQuery] DateTime date,
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .GetUsersActiveAfterAsync(date, pagination);

        return result.ToActionResult();
    }
}