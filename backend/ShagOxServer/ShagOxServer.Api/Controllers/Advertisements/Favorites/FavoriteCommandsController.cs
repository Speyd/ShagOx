using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Advertisements.Favorites.Create;
using ShagOxServer.Application.DTOs.Advertisements.Favorites.Update;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Favorites;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Create;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Delete;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Update;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Advertisements.Favorites;

[ApiController]
[Route("api/favorites")]
[Authorize]
public class FavoriteCommandsController
    : ApiOwnerExistsController
{
    private readonly IFavoriteCreateService _createService;
    private readonly IFavoriteUpdateService _updateService;
    private readonly IFavoriteDeleteService _deleteService;


    public FavoriteCommandsController(
        IFavoriteCreateService createService,
        IFavoriteUpdateService updateService,
        IFavoriteDeleteService deleteService,
        IFavoriteExistsRepository existsAdvertRepository,
        IUserAdminQueryService userQueryService
    )
        : base(existsAdvertRepository, userQueryService)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] FavoriteCreateRequest request)
    {
        var result = await _createService
            .CreateAsync(request, UserId);

        return result.ToActionResult();
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        [FromRoute] long id,
        [FromBody] FavoriteUpdateRequest request)
    {
        var forbidden = await CheckOwnershipAsync(id);
        if (forbidden is not null)
            return forbidden;

        var result = await _updateService
            .UpdateAsync(id, request);

        return result.ToActionResult();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(
        [FromRoute] long id)
    {
        var forbidden = await CheckOwnershipAsync(id);
        if (forbidden is not null)

            return forbidden;
        var result = await _deleteService
            .SoftDeleteAsync(id);

        return result.ToActionResult();
    }
}
