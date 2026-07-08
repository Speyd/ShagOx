using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Advertisements.Query;
using ShagOxServer.Application.Interfaces.Users.Query;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Users.Advertisements;

[ApiController]
[Authorize]
[Route("api/users/me/advertisements")]
public class UserAdvertisementMeController : ControllerBase
{
    private readonly IAdvertisementQueryService _queryService;
    private readonly IUserQueryService _queryUserService;


    public UserAdvertisementMeController(
        IAdvertisementQueryService queryService,
        IUserQueryService queryUserService)
    {
        _queryService = queryService;
        _queryUserService = queryUserService;
    }


    [HttpGet("sales")]
    public async Task<IActionResult> GetMySales(
        [FromQuery] PaginationParams pagination)
    {
        var user = await _queryUserService.GetMyProfileAsync();

        if (!user.IsSuccess || user.Value is null)
            return NotFound(user.Error);

        var result = await _queryService.GetSellerAdvertisementsAsync(user.Value.Id, pagination);

        return result.ToActionResult();
    }

    [HttpGet("purchases")]
    public async Task<IActionResult> GetMyPurchases(
        [FromQuery] PaginationParams pagination)
    {
        var user = await _queryUserService.GetMyProfileAsync();

        if (!user.IsSuccess || user.Value is null)
            return NotFound(user.Error);

        var result = await _queryService.GetPurchasedAdvertisementsAsync(user.Value.Id, pagination);

        return result.ToActionResult();
    }
}