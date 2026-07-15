using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Query;
using ShagOxServer.Application.Interfaces.Services.Users.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Advertisements.Admin;

[ApiController]
[Route("api/admin/advertisements")]
[Authorize(Roles = "Admin")]
public class AdvertisementAdminQueriesController : AdvertisementOwnerController
{
    private readonly IAdvertisementQueryService _queryAdvertService;

    public AdvertisementAdminQueriesController(
       IAdvertisementQueryService queryAdvertService,
       IAdvertisementExistsRepository existsAdvertRepository,
       IUserAdminQueryService userQueryService)
        :base(existsAdvertRepository, userQueryService)
    {
        _queryAdvertService = queryAdvertService;
    }

    [HttpGet("purchases/{userId:int}")]
    public async Task<IActionResult> GetPurchasedAdvertisements(
        [FromRoute] int userId,
        [FromQuery] PaginationParams pagination)
    {
        var exists = await _userQueryService.ExistsByIdAsync(userId);

        if (!exists)
            return NotFound("User not found");

        var advertisements =
            await _queryAdvertService.GetPurchasedAdvertisementsAsync(userId, pagination);

        return advertisements.ToActionResult();
    }
}
