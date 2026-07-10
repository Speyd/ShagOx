using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Services.Users.Query;

namespace ShagOxServer.Api.Controllers.Advertisements;

public abstract class AdvertisementOwnerController : ApiController
{
    protected readonly IUserAdminQueryService _userService;


    protected AdvertisementOwnerController(
        IUserAdminQueryService userService)
    {
        _userService = userService;
    }


    protected async Task<IActionResult?> CheckAdvertisementOwnerAsync(
        int advertisementId)
    {
        var isOwner = await _userService
            .IsAdvertisementOwnerAsync(
                UserId,
                advertisementId);


        if (!isOwner)
            return Forbid();


        return null;
    }
}