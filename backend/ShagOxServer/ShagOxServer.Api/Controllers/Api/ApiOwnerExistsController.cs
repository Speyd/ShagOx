using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.Interfaces.Repositories.Base.Special;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Api;
public abstract class ApiOwnerExistsController
    : ApiController
{
    protected readonly IExistsOwnerRepository _ownerRepository;
    protected readonly IUserAdminQueryService _userQueryService;

    public ApiOwnerExistsController(
        IExistsOwnerRepository ownerRepository,
        IUserAdminQueryService userQueryService)
    {
        _ownerRepository = ownerRepository;
        _userQueryService = userQueryService;
    }

    protected async Task<IActionResult?> CheckOwnershipAsync(
        long entityId)
    {
        var isOwner = await _ownerRepository
            .IsOwnerAsync(
                entityId,
                UserId);

        if (!isOwner)
            return Forbid();


        return null;
    }
}