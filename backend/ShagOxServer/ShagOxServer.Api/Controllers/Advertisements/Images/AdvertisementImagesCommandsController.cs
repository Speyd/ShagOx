using Microsoft.AspNetCore.Mvc;
using ShagOxServer.Application.DTOs.Advertisements.Update;
using ShagOxServer.Application.DTOs.Advertisements.Update.Images;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Images;
using ShagOxServer.Application.Interfaces.Services.Users.Query;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Api.Controllers.Advertisements.Images;

[ApiController]
[Route("api/advertisements/{advertisementId:int}/images")]

public class AdvertisementImagesCommandsController : AdvertisementOwnerController
{
    private readonly IAdvertisementImageService _imageService;


    public AdvertisementImagesCommandsController(
        IAdvertisementImageService imageService,
        IAdvertisementExistsRepository existsAdvertRepository, 
        IUserAdminQueryService userQueryService)
        : base(existsAdvertRepository, userQueryService)
    {
        _imageService = imageService;
    }


    [HttpPut]
    public async Task<IActionResult> SyncImages(
        [FromRoute] int advertisementId,
        [FromForm] AdvertisementUpdateRequest request)
    {
        var forbidden = await CheckAdvertisementOwnerAsync(advertisementId);
        if (forbidden is not null)
            return forbidden;

        var result = await _imageService
            .SyncImagesAsync(advertisementId, request);


        return result.ToActionResult();
    }


    [HttpPut("order")]
    public async Task<IActionResult> UpdateOrder(
        [FromRoute] int advertisementId,
        [FromBody] ImageOrderUpdateRequest request)
    {
        var forbidden = await CheckAdvertisementOwnerAsync(advertisementId);
        if (forbidden is not null)
            return forbidden;

        var result = await _imageService
            .UpdateImagesOrderAsync(
                advertisementId,
                request);


        return result.ToActionResult();
    }
}