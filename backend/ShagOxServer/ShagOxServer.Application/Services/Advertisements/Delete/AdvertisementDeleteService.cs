using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Advertisements.Delete;
using ShagOxServer.Application.Interfaces.Advertisements.Delete;
using ShagOxServer.Infrastructure.Interfaces.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Delete;
public class AdvertisementDeleteService : IAdvertisementDeleteService
{
    private readonly IAdvertisementRepository _repository;

    public AdvertisementDeleteService(
        IAdvertisementRepository advertisementRepository)
    {
        _repository = advertisementRepository;
    }

    public async Task<Result<AdvertisementDeleteResponse>> DeleteAdvertisementAsync(int id)
    {
        var advert = await _repository.GetByIdAsync(id);
        if (advert is null)
            return Result<AdvertisementDeleteResponse>.NotFound("Advertisement");

        await _repository.DeleteAsync(advert);
        return Result<AdvertisementDeleteResponse>.Success(
           new AdvertisementDeleteResponse(
               advert.Id,
               DateTime.UtcNow
           )
       );
    }
}