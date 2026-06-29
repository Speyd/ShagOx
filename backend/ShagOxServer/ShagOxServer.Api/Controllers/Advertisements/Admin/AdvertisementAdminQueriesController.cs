using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Advertisements.Query;
using ShagOxServer.Application.Interfaces.Users.Query;

namespace ShagOxServer.Api.Controllers.Advertisements.Admin;

[ApiController]
[Route("api/admin/advertisements")]
[Authorize(Roles = "Admin")]
public class AdvertisementAdminQueriesController : ControllerBase
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
        int userId)
    {
        var exists = await _queryUserService.ExistsAsync(userId);

        if (!exists)
            return NotFound("User not found");

        var advertisements =
            await _queryAdvertService.GetPurchasedAdvertisementsAsync(userId);

        return Ok(advertisements);
    }
}
