using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Base.Requests;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Favorites.Update;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Advertisements.Favorites.Admin;

[ApiController]
[Route("api/admin/favorites")]
[Authorize(Roles = "Admin")]
public class FavoriteAdminCommandsController
    : ApiController
{
    private readonly IFavoriteUpdateService _updateService;


    public FavoriteAdminCommandsController(
        IFavoriteUpdateService updateService,
        IUserContext userContext
    )
        : base(userContext)
    {
        _updateService = updateService;
    }

   
    [HttpPatch("{id:long}/status")]
    public async Task<IActionResult> ChangeStatus(
        [FromRoute] long id,
        [FromBody] ChangeStatusRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await _updateService
            .ChangeStatusAsync(id, request,
                cancellationToken);

        return result.ToActionResult();
    }
}
