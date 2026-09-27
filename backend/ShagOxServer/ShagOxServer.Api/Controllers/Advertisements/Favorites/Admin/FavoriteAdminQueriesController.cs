using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Advertisements.Favorites.Admin;

[ApiController]
[Route("api/admin/favorite")]
[Authorize]
public class FavoriteAdminQueriesController 
    : ApiController
{
    private readonly IFavoriteQueryService _queryService;


    public FavoriteAdminQueriesController(
        IFavoriteQueryService queryService)
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

    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery] PaginationParams pagination)
    {
        var result = await _queryService
            .GetPagedAsync(pagination);

        return result.ToActionResult();
    }

    [HttpGet("by-user/{userId:long}")]
    public async Task<IActionResult> GetByUserIdAsync(
        [FromRoute] long userId,
        [FromQuery] PaginationParams pagination
        )
    {
        var result = await _queryService
            .GetByUserAsync(userId, pagination);

        return result.ToActionResult();
    }
}