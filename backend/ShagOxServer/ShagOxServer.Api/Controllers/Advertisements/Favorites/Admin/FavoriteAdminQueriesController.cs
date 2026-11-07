using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Api.Controllers.Base.Query;
using ShagOxServer.Application.DTOs.Advertisements.Favorites.Query;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Query;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Advertisements.Favorites.Admin;

[ApiController]
[Route("api/admin/favorites")]
[Authorize(Roles = "Admin")]
public class FavoriteAdminQueriesController 
    : BaseAdminQueryController<FavoriteDto,
        FavoriteSearchFilter>
{
    private readonly IFavoriteQueryService _favoriteQueryService;


    public FavoriteAdminQueriesController(
        IFavoriteQueryService queryService,
        IUserContext userContext
    )
        : base(queryService, userContext)
    {
        _favoriteQueryService = queryService;
    }


    [HttpGet("by-user/{userId:long}")]
    public async Task<IActionResult> GetByUserIdAsync(
        [FromRoute] long userId,
        [FromQuery] PaginationParams pagination,
        [FromQuery] EntityStatus status,
        CancellationToken cancellationToken)
    {
        var result = await _favoriteQueryService
            .GetByUserAsync(userId, pagination,
                cancellationToken, status);

        return result.ToActionResult();
    }
}
