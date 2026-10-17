using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Delete;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Delete;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Mapping;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Validator;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Images.Delete;
public class AdvertisementImageDeleteService 
    : PictureDeleteService<Advertisement, AdvertisementImage>,
    IAdvertisementImageDeleteService
{
    private readonly ImageInvalidationService _imageInvalid;


    public AdvertisementImageDeleteService(
        IRepository<AdvertisementImage> imageRepository,
        AdvertisementImageValidator imageValidator,
        PictureValidator pictureValidator,
        ImageInvalidationService imageInvalid,
        IUnitOfWork unitOfWork,
        ILogger<AdvertisementImageDeleteService> logger
    )
        : base(imageRepository, imageValidator, pictureValidator, unitOfWork, logger)
    {
        _imageInvalid = imageInvalid;
    }


    public override async Task<Result<DeleteResponse>> DeleteAsync(
        long id)
    {
        try
        {
            return await base.DeleteAsync(id);
        }
        catch
        {

            return Result<DeleteResponse>
                .Fail(EntityErrorResources.AdvertImageDeleteFailed);
        }
    }
    public override async Task<Result<DeleteResponse>> DeleteRecordAsync(
        long id)
    {
       
        try
        {
            return await base.DeleteRecordAsync(id);
        }
        catch
        {
            return Result<DeleteResponse>
                .Fail(EntityErrorResources.AdvertImageDeleteFailed);
        }
    }


    public override async Task InvalidateCache(
        AdvertisementImage image)
    {
        await _imageInvalid.InvalidateDeleteAsync(
            AdvertisementImageCacheMapper.ToInfo(image));
    }
}
