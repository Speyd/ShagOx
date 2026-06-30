using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Specification.Images.Create;
using ShagOxServer.Application.Interfaces.Specification.Images.Create;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Infrastructure.Interfaces.Advertisements;
using ShagOxServer.Infrastructure.Interfaces.Specification;

namespace ShagOxServer.Application.Services.Specification.Images.Create;
public class ImageCreateService : IImageCreateService
{
    private readonly IImageRepository _imageRepository;
    private readonly IAdvertisementRepository _advertisementRepository;


    public ImageCreateService(
        IImageRepository imageRepository,
        IAdvertisementRepository advertisementRepository)
    {
        _imageRepository = imageRepository;
        _advertisementRepository = advertisementRepository;
    }

    public async Task<Result<ImageCreateResponse>> CreateImageAsync(ImageCreateRequest request)
    {
        var exists = await _advertisementRepository.ExistsById(request.AdvertisementId);
        if (!exists)
            Result<ImageCreateResponse>.NotFound("Advertisement");

        var image = CreateImage(request);

        await _imageRepository.AddAsync(image);

        var response = new ImageCreateResponse(
            image.Id,
            DateTime.UtcNow
        );

        return Result<ImageCreateResponse>.Success(response);
    }

    private Image CreateImage(
       ImageCreateRequest request)
    {
        return new Image
        {
            Url = request.Url,
            Order = request.Order,
            AdvertisementId = request.AdvertisementId,
        };
    }
}
