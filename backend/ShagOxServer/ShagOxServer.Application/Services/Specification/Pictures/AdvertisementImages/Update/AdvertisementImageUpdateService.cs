using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Request;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Update.Response;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Update;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Update;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Validator;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Images.Update;
public class AdvertisementImageUpdateService 
    : PictureOrderUpdateService<Advertisement,
        AdvertisementImage,
        PictureOrderUpdateRequest>,
    IAdvertisementImageUpdateService
{
    public AdvertisementImageUpdateService(
        IRepository<AdvertisementImage> imageRepository,
        PictureValidator pictureLoadValidator,
        AdvertisementImageValidator imageValidator,
        IAdvertisementImageDeleteService imageDeleteService,
        AdvertisementValidator advertisementValidator,
        IUnitOfWork unitOfWork,
        ILogger<AdvertisementImageUpdateService> logger
    )
        : base(imageRepository,
            pictureLoadValidator,
            imageDeleteService,
            imageValidator,
            advertisementValidator,
            unitOfWork, 
            logger)
    {
    }

   
    public override async Task<Result<PictureUpdateResponse>> UpdateAsync(
        long imageId,
        PictureOrderUpdateRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            
            return await base.UpdateAsync(imageId, 
                request, cancellationToken);
        }
        catch
        {
            return Result<PictureUpdateResponse>.Fail(
                EntityErrorResources.AdvertImageUpdateFailed);
        }
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
