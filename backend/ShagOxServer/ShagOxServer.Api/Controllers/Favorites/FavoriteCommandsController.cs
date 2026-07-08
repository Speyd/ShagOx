using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Advertisements.Favorites.Create;
using ShagOxServer.Application.DTOs.Advertisements.Favorites.Update;
using ShagOxServer.Application.Interfaces.Advertisements.Favorites.Create;
using ShagOxServer.Application.Interfaces.Advertisements.Favorites.Delete;
using ShagOxServer.Application.Interfaces.Advertisements.Favorites.Update;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;
using System.Security.Claims;

namespace ShagOxServer.Api.Controllers.Favorites;

[ApiController]
[Route("api/favorite")]
[Authorize]
public class FavoriteCommandsController : ControllerBase
{
    private readonly IFavoriteCreateService _createService;
    private readonly IFavoriteUpdateService _updateService;
    private readonly IFavoriteDeleteService _deleteService;

    public FavoriteCommandsController(
        IFavoriteCreateService createService,
        IFavoriteUpdateService updateService,
        IFavoriteDeleteService deleteService
        )
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] FavoriteCreateRequest request)
    {
        var userId = int.Parse(
           User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        if (request.UserId != userId)
            return Forbid();

        var result = await _createService.CreateFavoriteAsync(request);
        return result.ToActionResult();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        [FromBody] FavoriteUpdateRequest request)
    {
        var result = await _updateService.UpdateFavoriteAsync(id, request);
        return result.ToActionResult();
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        [FromRoute] int id)
    {
        var result = await _deleteService.DeleteFavoriteAsync(id);
        return result.ToActionResult();
    }
}