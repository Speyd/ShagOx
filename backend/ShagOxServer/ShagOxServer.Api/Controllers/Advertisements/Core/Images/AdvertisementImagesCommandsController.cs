using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Api.Controllers.Api;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Request;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Core;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Create;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Advertisements.Core.Images;

[ApiController]
[Route("api/advertisements/{advertisementId:int}/images")]
[Authorize(Roles = "Admin")]
public class AdvertisementImagesCommandsController 
    : ApiOwnerExistsController
{
    private readonly IPictureOrderingService<Advertisement,
        AdvertisementImage> _imageService;

    private readonly IAdvertisementImageCreateService _imageCreate;


    public AdvertisementImagesCommandsController(
        IPictureOrderingService<Advertisement,AdvertisementImage> imageService,
        IAdvertisementImageCreateService imageCreate,
        IAdvertisementExistsRepository existsAdvertRepository, 
        IUserAdminQueryService userQueryService
    ) : base(existsAdvertRepository, userQueryService)
    {
        _imageService = imageService;
        _imageCreate = imageCreate;
    }


    [HttpPut]
    public async Task<IActionResult> SyncImages(
        [FromRoute] long advertisementId,
        [FromForm] List<OwnerPictureOrderUpdateRequest> request)
    {
        var forbidden = await CheckOwnershipAsync(advertisementId);
        if (forbidden is not null)
            return forbidden;

        var result = await _imageService
            .SyncPicturesAsync(advertisementId, 
                request);


        return result.ToActionResult();
    }
}
