using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Query;
using ShagOxServer.Application.Interfaces.Services.Users.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Users.Advertisements;

[ApiController]
[Route("api/users/{userId:int}/advertisements")]
public class UserAdvertisementsController : ApiController
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


    [HttpGet]
    public async Task<IActionResult> GetUserAdvertisements(
        [FromRoute] int userId,
        [FromQuery] PaginationParams pagination)
    {
        var exists = await _userQuery
            .ExistsByIdAsync(userId);

        if (!exists)
            return NotFound("User not found");

        var result = await _queryService
            .GetBySellerAsync(userId, pagination);

        return result.ToActionResult();
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("purchases")]
    public async Task<IActionResult> GetPurchasedAdvertisements(
        [FromRoute] int userId,
        [FromQuery] PaginationParams pagination)
    {
        var exists = await _userQuery
            .ExistsByIdAsync(userId);

        if (!exists)
            return NotFound("User not found");

        var advertisements = await _queryService.
            GetPurchasedByUserAsync(userId, pagination);

        return advertisements.ToActionResult();
    }
}