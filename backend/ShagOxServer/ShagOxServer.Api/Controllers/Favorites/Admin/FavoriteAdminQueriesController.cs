using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Advertisements.Favorites.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Favorites.Admin;

[ApiController]
[Route("api/admin/favorite")]
[Authorize]
public class FavoriteAdminQueriesController : ApiController
{
    private readonly IFavoriteQueryService _queryService;

    public FavoriteAdminQueriesController(
        IFavoriteQueryService queryService)
    {
        _queryService = queryService;
    }

    [HttpGet("by-user/{userId:int}")]
    public async Task<IActionResult> GetByUserIdAsync(
        [FromRoute] int userId,
        [FromQuery] PaginationParams pagination
        )
    {
        var result = await _queryService.GetByUserIdAsync(
            userId,
            pagination);

        return result.ToActionResult();
    }
}