using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Advertisements.Query;
using ShagOxServer.Application.Interfaces.Users.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Advertisements.Admin;

[ApiController]
[Route("api/admin/advertisements")]
[Authorize(Roles = "Admin")]
public class AdvertisementAdminQueriesController : ApiController
{
    private readonly IAdvertisementQueryService _queryAdvertService;
    private readonly IUserAdminQueryService _queryUserService;

    public AdvertisementAdminQueriesController(
       IAdvertisementQueryService queryAdvertService,
       IUserAdminQueryService queryUserService)
    {
        _queryAdvertService = queryAdvertService;
        _queryUserService = queryUserService;
    }

    [HttpGet("purchases/{userId:int}")]
    public async Task<IActionResult> GetPurchasedAdvertisements(
        [FromRoute] int userId,
        [FromQuery] PaginationParams pagination)
    {
        var exists = await _queryUserService.ExistsAsync(userId);

        if (!exists)
            return NotFound("User not found");

        var advertisements =
            await _queryAdvertService.GetPurchasedAdvertisementsAsync(userId, pagination);

        return advertisements.ToActionResult();
    }
}
