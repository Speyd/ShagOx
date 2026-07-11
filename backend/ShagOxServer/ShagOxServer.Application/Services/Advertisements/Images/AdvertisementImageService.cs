using ShagOxServer.Application.DTOs.Advertisements.Update;
using ShagOxServer.Application.DTOs.Advertisements.Update.Images;
using ShagOxServer.Application.DTOs.Specification.Images.Create;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Images;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Images;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Images.Create;
using ShagOxServer.Application.Interfaces.Services.Roles.Specification.Images.Delete;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Images;
public class AdvertisementImageService : IAdvertisementImageService
{
    private readonly IImageQueryRepository _imageQueryRepository;
    private readonly IImageCreateService _imageCreateService;
    private readonly IImageDeleteService _imageDeleteService;
    private readonly IAdvertisementQueryRepository _advertRepository;


    private readonly IUnitOfWork _unitOfWork;


    public AdvertisementImageService(
        IImageQueryRepository imageQueryRepository,
        IImageCreateService imageCreateService,
        IImageDeleteService imageDeleteService,
        IAdvertisementQueryRepository advertRepository,
        IUnitOfWork unitOfWork)
    {
        _imageQueryRepository = imageQueryRepository;
        _imageCreateService = imageCreateService;
        _imageDeleteService = imageDeleteService;
        _advertRepository = advertRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> UpdateImagesOrderAsync(
       int advertisementId,
       ImageOrderUpdateRequest request)
    {
        var images = await _imageQueryRepository
            .GetByAdvertisementIdAsync(advertisementId);


        if (images.Count != request.ImageIds.Count)
        {
            return Result<bool>
                .Fail("Image count does not match");
        }


        var imagesById = images
            .ToDictionary(x => x.Id);


        foreach (var imageId in request.ImageIds)
        {
            if (!imagesById.ContainsKey(imageId))
            {
                return Result<bool>
                    .Fail($"Image {imageId} does not belong to advertisement");
            }
        }


        await _unitOfWork.BeginTransactionAsync();

        try
        {
            foreach (var image in images)
            {
                image.Order += 1000;
            }

            await _unitOfWork.SaveChangesAsync();

            for (int order = 0; order < request.ImageIds.Count; order++)
            {
                var image = imagesById[request.ImageIds[order]];
                image.Order = order;
            }

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }


        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> SyncImagesAsync(
       int advertisementId,
       AdvertisementUpdateRequest request)
    {
        var advertisement = await _advertRepository
            .GetByIdAsync(advertisementId);

        if (advertisement is null)
            return Result<bool>.NotFound("Advertisement");

        return await SyncImagesAsync(advertisement, request);
    }

    public async Task<Result<bool>> SyncImagesAsync(
        Advertisement advertisement,
        AdvertisementUpdateRequest request)
    {
        if (request.DeletedImageIds is not null)
        {
            foreach (var imageId in request.DeletedImageIds)
            {
                var image = advertisement.Images
                    .FirstOrDefault(x => x.Id == imageId);

                if (image is null)
                    continue;

                var result = await _imageDeleteService
                    .DeleteImageAsync(imageId);

                if (!result.IsSuccess)
                    return Result<bool>.Fail(result.Error!);

                advertisement.Images.Remove(image);
            }

            RecalculateImagesOrder(advertisement);
        }

        if (request.NewImages is not null)
        {
            var order = advertisement.Images.Count;

            foreach (var file in request.NewImages)
            {
                if (file is null)
                    continue;

                var result = await _imageCreateService.CreateFromFileAsync(
                    new ImageFileCreateRequest(
                        file,
                        advertisement.Id,
                        order++));

                if (!result.IsSuccess)
                    return Result<bool>.Fail(result.Error!);
            }
        }

        return Result<bool>.Success(true);
    }


    public async Task RecalculateImagesOrderAsync(
        int advertisementId)
    {
        var images = await _imageQueryRepository
            .GetByAdvertisementIdAsync(advertisementId);


        var order = 0;

        foreach (var image in images.OrderBy(x => x.Order))
        {
            image.Order = order++;
        }
    }

    private static void RecalculateImagesOrder(
        Advertisement advertisement)
    {
        int order = 0;

        foreach (var image in advertisement.Images.OrderBy(x => x.Order))
        {
            image.Order = order++;
        }
    }
}