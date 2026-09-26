using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Favorites;

[ApiController]
[Route("api/favorite")]
public class FavoriteQueriesController 
    : ApiController
{
    private readonly IFavoriteQueryService _queryService;


    public FavoriteQueriesController(
        IFavoriteQueryService queryService)
    {
        _queryService = queryService;
    }


    [Authorize]
    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(
        [FromRoute] long id)
    {
        var result = await _queryService
            .GetByIdAsync(id);

        return result.ToActionResult();
    }

    [HttpGet("count/{advertId:long}")]
    public async Task<IActionResult> CountByAdvertisementIdAsync(
         [FromRoute] long advertId)
    {
        var result = await _queryService
            .CountByAdvertisementAsync(advertId);

        return result.ToActionResult();
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMyfavorite(
       [FromQuery] PaginationParams pagination
       )
    {
        var result = await _queryService
            .GetByUserAsync(UserId, pagination);

        return result.ToActionResult();
    }
}