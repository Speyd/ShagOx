using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Advertisements.Query;
using ShagOxServer.Application.Interfaces.Users.Query;

namespace ShagOxServer.Api.Controllers.Users.Advertisements;

[ApiController]
[Route("api/users/{userId:int}/advertisements")]
public class UserAdvertisementsController : ControllerBase
{
    private readonly IAdvertisementQueryService _queryService;
    private readonly IUserAdminQueryService _userQuery;

    public UserAdvertisementsController(
        IAdvertisementQueryService queryService,
        IUserAdminQueryService userQuery)
    {
        _queryService = queryService;
        _userQuery = userQuery;
    }

    //TODO: Refactore code 'exists'
    [HttpGet]
    public async Task<IActionResult> GetUserAdvertisements(
        [FromRoute] int userId,
        [FromQuery] int page,
        [FromQuery] int pageSize)
    {
        var exists = await _userQuery.ExistsAsync(userId);

        if (!exists)
            return NotFound();

        return Ok(await _queryService.GetSellerAdvertisementsAsync(userId, page, pageSize));
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("purchases")]
    public async Task<IActionResult> GetPurchasedAdvertisements(
      [FromRoute] int userId,
        [FromQuery] int page,
        [FromQuery] int pageSize)
    {
        var exists = await _userQuery.ExistsAsync(userId);

        if (!exists)
            return NotFound("User not found");

        var advertisements =
            await _queryService.GetPurchasedAdvertisementsAsync(userId, page, pageSize);

        return Ok(advertisements);
    }
}