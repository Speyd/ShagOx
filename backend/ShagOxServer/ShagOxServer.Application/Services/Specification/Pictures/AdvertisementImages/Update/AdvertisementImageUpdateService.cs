using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Request;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Response;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Update;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Update;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Mapping;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Validator;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;
using Twilio.Http;

namespace ShagOxServer.Application.Services.Specification.Pictures.Images.Update;
public class AdvertisementImageUpdateService 
    : PictureOrderUpdateService<Advertisement,
        AdvertisementImage,
        PictureOrderUpdateRequest>,
    IAdvertisementImageUpdateService
{
    private readonly ImageInvalidationService _imageInvalid;


    public AdvertisementImageUpdateService(
        IRepository<AdvertisementImage> imageRepository,
        PictureValidator pictureLoadValidator,
        AdvertisementImageValidator imageValidator,
        AdvertisementValidator advertisementValidator,
        ImageInvalidationService imageInvalid,
        IUnitOfWork unitOfWork,
        ILogger<AdvertisementImageUpdateService> logger
    )
        : base(imageRepository,
            pictureLoadValidator,
            imageValidator,
            advertisementValidator,
            unitOfWork, 
            logger)
    {
        _imageInvalid = imageInvalid;
    }

   
    public override async Task<Result<PictureUpdateResponse>> UpdateAsync(
        long imageId,
        PictureOrderUpdateRequest request)
    {
        try
        {
            
            return await base.UpdateAsync(imageId, request);
        }
        catch
        {
            return Result<PictureUpdateResponse>.Fail(
                EntityErrorResources.AdvertImageUpdateFailed);
        }
    }

    public override async Task InvalidateCache(
        AdvertisementImage picture)
    {
        await _imageInvalid.InvalidateUpdateAsync(
           AdvertisementImageCacheMapper.ToInfo(picture));
    }

    protected override int ApplyUpdates(
        AdvertisementImage picture,
        PictureOrderUpdateRequest request,
        PictureLoaderUploadResponse? response)
    {
        if (response is not null)
        {
            return AdvertisementImageUpdater.ApplyUpdates(
                picture,
                request,
                response
            );
        }

        return AdvertisementImageUpdater.ApplyUpdates(
            picture,
            request
        );
    }
}
