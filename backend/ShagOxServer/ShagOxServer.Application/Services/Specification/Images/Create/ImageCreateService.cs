using ShagOxServer.Application.DTOs.Common.ImageLoaders.Upload;
using ShagOxServer.Application.DTOs.Specification.Images.Create;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Images;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Images.Create;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Specification.Images.Create;
public class ImageCreateService : IImageCreateService
{
    private readonly IImageRepository _imageRepository;
    private readonly IImageQueryRepository _imageQueryRepository;
    private readonly IAdvertisementRepository _advertisementRepository;
    private readonly IImageLoaderService _loaderService;

    public ImageCreateService(
        IImageRepository imageRepository,
        IImageQueryRepository imageQueryRepository,
        IAdvertisementRepository advertisementRepository,
        IImageLoaderService loaderService)
    {
        _imageRepository = imageRepository;
        _imageQueryRepository = imageQueryRepository;
        _advertisementRepository = advertisementRepository;
        _loaderService = loaderService;
    }


    public async Task<Result<ImageCreateResponse>> CreateAsync(
        ImageCreateRequest request)
    {
        var advert = await _advertisementRepository.GetByIdAsync(request.AdvertisementId);
        if (advert is null)
            return Result<ImageCreateResponse>.NotFound("Advertisement");

        var orderExists = advert.Images.Any(x => x.Order == request.Order);
        var newOrder = await _imageQueryRepository
            .GetNextOrder(request.AdvertisementId, request.Order);

        var image = CreateImage(newOrder, request);

        await _imageRepository.AddAsync(image);

        return Result<ImageCreateResponse>.Success(
            new ImageCreateResponse(
            image.Id,
            image.PublicId,
            DateTime.UtcNow)
        );
    }

    public async Task<Result<ImageCreateResponse>> CreateFromFileAsync(
        ImageFileCreateRequest request)
    {
        var advert = await _advertisementRepository.GetByIdAsync(request.AdvertisementId);
        if (advert is null)
            return Result<ImageCreateResponse>.NotFound("Advertisement");

        var newOrder = await _imageQueryRepository
            .GetNextOrder(request.AdvertisementId);

        var response = await _loaderService.UploadAsync(request.File);
        if (!response.IsSuccess || response.Value is null)
            return Result<ImageCreateResponse>.Fail("Fail Upload Image");

        var image = CreateImage(newOrder, request, response.Value);

        await _imageRepository.AddAsync(image);

        return Result<ImageCreateResponse>.Success(
            new ImageCreateResponse(
            image.Id,
            image.PublicId,
            DateTime.UtcNow)
        );
    }

    public async Task<Result<ImageCreateResponse>> CreateFromFileInternalAsync(
        ImageFileCreateRequest request)
    {
        var newOrder = await _imageQueryRepository
            .GetNextOrder(request.AdvertisementId);

        var uploadResult = await _loaderService
            .UploadAsync(request.File);

        if (!uploadResult.IsSuccess || uploadResult.Value is null)
        {
            return Result<ImageCreateResponse>
                .Fail("Fail Upload Image");
        }

        var image = CreateImage(
            newOrder,
            request,
            uploadResult.Value);


        await _imageRepository.AddAsync(image);


        return Result<ImageCreateResponse>.Success(
            new ImageCreateResponse(
                image.Id,
                image.PublicId,
                DateTime.UtcNow)
        );
    }

    private Image CreateImage(
        int newOrder,
        ImageCreateRequest request)
    {
        return new Image
        {
            Url = request.Url,
            Order = newOrder,
            AdvertisementId = request.AdvertisementId,
            PublicId = ""
        };
    }

    private Image CreateImage(
        int newOrder,
        ImageFileCreateRequest request,
        ImageLoaderUploadResponse response)
    {
        return new Image
        {
            Url = response.Url,
            Order = newOrder,
            AdvertisementId = request.AdvertisementId,
            PublicId = response.PublicId
        };
    }
}
