using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;
using ShagOxServer.Api.Controllers.Api;

namespace ShagOxServer.Api.Controllers.Advertisements.Core.Admin;

[ApiController]
[Route("api/admin/advertisements")]
[Authorize(Roles = "Admin")]
public class AdvertisementAdminQueriesController 
    : ApiOwnerExistsController
{
    private readonly IAdvertisementQueryService _queryAdvertService;


    public AdvertisementAdminQueriesController(
       IAdvertisementQueryService queryAdvertService,
       IAdvertisementExistsRepository existsAdvertRepository,
       IUserAdminQueryService userQueryService
    ) : base(existsAdvertRepository, userQueryService)
    {
        _queryAdvertService = queryAdvertService;
    }


    [HttpGet("purchases/{userId:long}")]
    public async Task<IActionResult> GetPurchasedAdvertisements(
        [FromRoute] long userId,
        [FromQuery] PaginationParams pagination)
    {
        var advertisements = await _queryAdvertService
            .GetPurchasedByUserAsync(userId, pagination);

        return advertisements.ToActionResult();
    }
}