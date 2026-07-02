using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Specification.Images.Create;
using ShagOxServer.Application.Interfaces.Specification.Images.Create;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Infrastructure.Interfaces.Advertisements;
using ShagOxServer.Infrastructure.Interfaces.Specification.Images;

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

    public async Task<Result<ImageCreateResponse>> CreateImageAsync(
        ImageCreateRequest request)
    {
        var advert = await _advertisementRepository.GetByIdAsync(request.AdvertisementId);
        if (advert is null)
            return Result<ImageCreateResponse>.NotFound("Advertisement");

        var orderExists = advert.Images.Any(x => x.Order == request.Order);
        var newOrder = request.Order;
        if (orderExists)
        {
            newOrder = advert.Images
                .Select( x => x.Order)
                .DefaultIfEmpty(0)
                .Max() + 1;
        }

        var image = CreateImage(newOrder, request);

        await _imageRepository.AddAsync(image);

        var response = new ImageCreateResponse(
            image.Id,
            DateTime.UtcNow
        );

        return Result<ImageCreateResponse>.Success(response);
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
        };
    }
}
