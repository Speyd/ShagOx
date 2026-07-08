using ShagOxServer.Application.DTOs.Advertisements.Delete;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Delete;
using ShagOxServer.Application.Interfaces.Services.Common.ImageLoaders;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Advertisements.Delete;
public class AdvertisementDeleteService : IAdvertisementDeleteService
{
    private readonly IAdvertisementRepository _repository;
    private readonly IAdvertisementQueryRepository _repositoryQuery;
    private readonly IImageLoaderService _imageService;

    public AdvertisementDeleteService(
        IAdvertisementRepository advertisementRepository,
        IAdvertisementQueryRepository advertisementQueryRepositor,
        IImageLoaderService imageService)
    {
        _repository = advertisementRepository;
        _repositoryQuery = advertisementQueryRepositor;
        _imageService = imageService;
    }

    public async Task<Result<AdvertisementDeleteResponse>> DeleteAdvertisementAsync(int id)
    {
        var advert = await _repositoryQuery.GetByIdAsync(id);
        if (advert is null)
            return Result<AdvertisementDeleteResponse>.NotFound("Advertisement");

        await _repository.DeleteAsync(advert);

        if (advert.Images.Count != 0)
        {
            foreach (var image in advert.Images)
            {
                await _imageService.DeleteAsync(image.PublicId);
            }
        }

        return Result<AdvertisementDeleteResponse>.Success(
           new AdvertisementDeleteResponse(
               advert.Id,
               DateTime.UtcNow
           )
       );
    }
}