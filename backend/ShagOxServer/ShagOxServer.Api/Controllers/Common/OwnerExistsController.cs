using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Repositories.Base.Special;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Query;

namespace ShagOxServer.Api.Controllers.Common;

public abstract class OwnerExistsController
    : ApiController
{
    protected readonly IExistsOwnerRepository _ownerRepository;
    protected readonly IUserAdminQueryService _userQueryService;

    public OwnerExistsController(
        IExistsOwnerRepository ownerRepository,
        IUserAdminQueryService userQueryService)
    {
        _ownerRepository = ownerRepository;
        _userQueryService = userQueryService;
    }

    protected async Task<IActionResult?> CheckAdvertisementOwnerAsync(
        int advertisementId)
    {
        var isOwner = await _ownerRepository
            .IsOwnerAsync(
                advertisementId,
                UserId);

        if (!isOwner)
            return Forbid();


        return null;
    }

}
