using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Request;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Create.Response;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Advertisements.Core.Validator;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Create;
using ShagOxServer.Application.Services.Specification.Pictures.Core.Validator;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Pictures.Images.Create;
public partial class AdvertisementImageCreateService 
    : PictureOrderCreateService<Advertisement,
        AdvertisementImage, 
        PictureOrderCreateRequest>,
    IAdvertisementImageCreateService
{
    public AdvertisementImageCreateService(
        IRepository<AdvertisementImage> imageRepository,
        PictureValidator pictureValidator,
        AdvertisementValidator advertValidator,
        IUnitOfWork unitOfWork,
        ILogger<AdvertisementImageCreateService> logger
    )
        : base(imageRepository, 
            pictureValidator, 
            advertValidator,
            unitOfWork, 
            logger)
    {
    }


    public override async Task<Result<PictureCreateResponse>> CreateAsync(
        PictureOrderCreateRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await base
                .CreateAsync(request, cancellationToken);
        }
        catch
        {
            return Result<PictureCreateResponse>.Fail(
                EntityErrorResources.AdvertImageCreateFailed);
        }
    }

    public override async Task<Result<PictureCreateResponse>> CreateInternalAsync(
        PictureOrderCreateRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await base
                .CreateInternalAsync(request, cancellationToken);
        }
        catch
        {
            return Result<PictureCreateResponse>.Fail(
                EntityErrorResources.AdvertImageCreateFailed);
        }
    }

    public override async Task<Result<PicturesCreateResponse>> CreateAsync(
        PicturesCreateRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await base
                .CreateAsync(request, cancellationToken);
        }
        catch
        {
            return Result<PicturesCreateResponse>.Fail(
                EntityErrorResources.AdvertImageCreateFailed);
        }
    }

    protected override AdvertisementImage CreatePicture(
        PictureOrderCreateRequest request,
        PictureLoaderUploadResponse response)
    {
        return AdvertisementImageCreater
            .Create(request, response);
    }

    protected override PictureOrderCreateRequest CreatePictureOrderRequest(
        IFormFile file, 
        long ownerId,
        int order)
    {
        return new PictureOrderCreateRequest(
            file,
            ownerId,
            order
        );
    }
}
