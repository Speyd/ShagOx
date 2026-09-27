using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Auth.Users.Advertisements;

[ApiController]
[Route("api/users/{userId:int}/advertisements")]
public class UserAdvertisementsController 
    : ApiController
{
    private readonly IAdvertisementQueryService _queryService;

    public UserAdvertisementsController(
        IAdvertisementQueryService queryService)
    {
        _queryService = queryService;
    }


    [HttpGet]
    public async Task<IActionResult> GetUserAdvertisements(
        [FromRoute] int userId,
        [FromQuery] PaginationParams pagination)
    {
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
        var advertisements = await _queryService.
            GetPurchasedByUserAsync(userId, pagination);

        return advertisements.ToActionResult();
    }
}