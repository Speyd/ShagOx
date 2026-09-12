using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Common;
using ShagOxServer.Application.DTOs.Advertisements.Core.Create;
using ShagOxServer.Application.DTOs.Advertisements.Core.Update;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Delete;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Update;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Query;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;
using System.Security.Claims;

namespace ShagOxServer.Api.Controllers.Advertisements;

[ApiController]
[Route("api/advertisements")]
[Authorize]
public class AdvertisementCommandsController 
    : OwnerExistsController
{
    private readonly IAdvertisementCreateService _createService;
    private readonly IAdvertisementUpdateService _updateService;
    private readonly IAdvertisementDeleteService _deleteService;


    public AdvertisementCommandsController(
        IAdvertisementCreateService createService,
        IAdvertisementUpdateService updateService,
        IAdvertisementDeleteService deleteService,
        IAdvertisementExistsRepository existsAdvertRepository,
        IUserAdminQueryService userQueryService
    ) : base(existsAdvertRepository, userQueryService)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }

    
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromForm] AdvertisementCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request, UserId);

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

        var result = await _updateService
            .UpdateAsync(id, request);

        return result.ToActionResult();
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id)
    {
        var claim = User
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var forbidden = await CheckAdvertisementOwnerAsync(id);
        if (forbidden is not null)
            return forbidden;

        var result = await _deleteService
            .DeleteAsync(id);

        return result.ToActionResult();
    }
}