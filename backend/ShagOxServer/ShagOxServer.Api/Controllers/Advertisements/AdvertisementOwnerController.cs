using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Query;

namespace ShagOxServer.Api.Controllers.Advertisements;
public abstract class AdvertisementOwnerController 
    : ApiController
{
    protected readonly IAdvertisementExistsRepository _advertExistsService;

    protected readonly IUserAdminQueryService _userQueryService;


    protected AdvertisementOwnerController(
        IAdvertisementExistsRepository advertExistsService,
        IUserAdminQueryService userQueryService)
    {
        _advertExistsService = advertExistsService;
        _userQueryService = userQueryService;
    }


    protected async Task<IActionResult?> CheckAdvertisementOwnerAsync(
        int advertisementId)
    {
        var isOwner = await _advertExistsService
            .IsOwnerAsync(
                advertisementId,
                UserId);

        if (!isOwner)
            return Forbid();


        return null;
    }
}