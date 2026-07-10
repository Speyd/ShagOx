using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Favorites;

[ApiController]
[Route("api/favorite")]
public class FavoriteQueriesController : ApiController
{
    private readonly IFavoriteQueryService _queryService;

    public FavoriteQueriesController(
        IFavoriteQueryService queryService)
    {
        _queryService = queryService;
    }


    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        [FromRoute] int id)
    {
        var result = await _queryService.GetByIdAsync(id);
        return result.ToActionResult();
    }

    [HttpGet("count/{advertId:int}")]
    public async Task<IActionResult> CountByAdvertisementIdAsync(
         [FromRoute] int advertId)
    {
        var result = await _queryService.CountByAdvertisementIdAsync(advertId);
        return result.ToActionResult();
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMyfavorite(
       [FromQuery] PaginationParams pagination
       )
    {
        var result = await _queryService.GetByUserIdAsync(
            UserId,
            pagination);

        return result.ToActionResult();
    }
}