using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Create;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Update;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Create;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Delete;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Update;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Advertisements.AdvertisementVariants;

[ApiController]
[Route("api/advertisement-variants")]
[Authorize]
public class AdvertisementVariantCommandsController
    : ApiOwnerExistsController
{
    private readonly IAdvertisementVariantCreateService _createService;
    private readonly IAdvertisementVariantUpdateService _updateService;
    private readonly IAdvertisementVariantDeleteService _deleteService;

    private readonly IAdvertisementVariantExistsRepository _existsAdvertRepository;


    public AdvertisementVariantCommandsController(
        IAdvertisementVariantCreateService createService,
        IAdvertisementVariantUpdateService updateService,
        IAdvertisementVariantDeleteService deleteService,
        IAdvertisementVariantExistsRepository existsVariantRepository,
        IAdvertisementVariantExistsRepository existsAdvertRepository,
        IUserAdminQueryService userQueryService
    )
        : base(existsVariantRepository, userQueryService)
    {
        _createService = createService;
        _updateService = updateService;
        _deleteService = deleteService;
        _existsAdvertRepository = existsAdvertRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        AdvertisementVariantCreateRequest request)
    {
        if(!await _existsAdvertRepository
            .IsOwnerAsync(request.AdvertisementId, UserId))
        {
            return Forbid();
        }

        var result = await _createService
            .CreateAsync(request);

        return result.ToActionResult();
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        [FromRoute] long id,
        [FromBody] AdvertisementVariantUpdateRequest request)
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
            .DeleteAsync(id);

        return result.ToActionResult();
    }
}