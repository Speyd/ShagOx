using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Advertisements.Create;
using ShagOxServer.Application.DTOs.Advertisements.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Create;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Delete;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Update;
using ShagOxServer.Application.Interfaces.Services.Users.Query;

namespace ShagOxServer.Api.Controllers.Advertisements;

[ApiController]
[Route("api/advertisements")]
[Authorize]
public class AdvertisementCommandsController : AdvertisementOwnerController
{
    private readonly IAdvertisementCreateService _createService;
    private readonly IAdvertisementUpdateService _updateService;
    private readonly IAdvertisementDeleteService _deleteService;

    public AdvertisementCommandsController(
        IAdvertisementCreateService createService,
        IAdvertisementUpdateService updateService,
        IAdvertisementDeleteService deleteService,
        IUserAdminQueryService userService)
        :base(userService)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }

    
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromForm] AdvertisementCreateRequest request)
    {
        if (request.SellerId != UserId)
            return Forbid();

        var result = await _createService.CreateAdvertisementAsync(request);
        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromForm] AdvertisementUpdateRequest request)
    {
        var forbidden = await CheckAdvertisementOwnerAsync(id);
        if (forbidden is not null)
            return forbidden;

        var result = await _updateService.UpdateAdvertisementAsync(id, request);
        return result.ToActionResult();
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id)
    {
        var forbidden = await CheckAdvertisementOwnerAsync(id);
        if (forbidden is not null)
            return forbidden;

        var result = await _deleteService.DeleteAdvertisementAsync(id);
        return result.ToActionResult();
    }
}