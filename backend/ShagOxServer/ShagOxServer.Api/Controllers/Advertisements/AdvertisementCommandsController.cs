using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Advertisements.Create;
using ShagOxServer.Application.DTOs.Advertisements.Update;
using ShagOxServer.Application.Interfaces.Advertisements.Create;
using ShagOxServer.Application.Interfaces.Advertisements.Delete;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.Interfaces.Advertisements.Update;
using ShagOxServer.Application.Interfaces.Users.Query;
using System.Security.Claims;

namespace ShagOxServer.Api.Controllers.Advertisements;

[ApiController]
[Route("api/advertisements")]
[Authorize]
public class AdvertisementCommandsController : ControllerBase
{
    private readonly IAdvertisementCreateService _createService;
    private readonly IAdvertisementUpdateService _updateService;
    private readonly IAdvertisementDeleteService _deleteService;
    private readonly IUserAdminQueryService _userService;

    public AdvertisementCommandsController(
        IAdvertisementCreateService createService,
        IAdvertisementUpdateService updateService,
        IAdvertisementDeleteService deleteService,
        IUserAdminQueryService userService)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
        _userService = userService;
    }

    
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] AdvertisementCreateRequest request)
    {
        var userId = int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        if (request.SellerId != userId)
            return Forbid();

        var result = await _createService.CreateAdvertisementAsync(request);
        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] AdvertisementUpdateRequest request)
    {
        var forbidden = await CheckAccess(id);
        if (forbidden is not null)
            return forbidden;

        var result = await _updateService.UpdateAdvertisementAsync(id, request);
        return result.ToActionResult();
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id)
    {
        var forbidden = await CheckAccess(id);
        if (forbidden is not null)
            return forbidden;

        var result = await _deleteService.DeleteAdvertisementAsync(id);
        return result.ToActionResult();
    }

    private async Task<IActionResult?> CheckAccess(int advertisementId)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var isOwner = await _userService.IsAdvertisementOwnerAsync(userId, advertisementId);

        if (!isOwner)
            return Forbid();

        return null;
    }
}
