using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Advertisements.Update;
using ShagOxServer.Application.Interfaces.Advertisements.Update;
using ShagOxServer.Domain.Entities;
using ShagOxServer.Infrastructure.Interfaces.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Update;
public class AdvertisementUpdateService : IAdvertisementUpdateService
{
    private readonly IAdvertisementRepository _repository;

    public AdvertisementUpdateService(
        IAdvertisementRepository advertisementRepository)
    {
        _repository = advertisementRepository;
    }

    public async Task<Result<AdvertisementUpdateResponse>> UpdateAdvertisementAsync(
        int advertId,
        AdvertisementUpdateRequest request)
    {
        var advert = await _repository.GetByIdAsync(advertId);

        if (advert is null)
            return Result<AdvertisementUpdateResponse>.NotFound("Advertisement");

        var updatedCount = ApplyUpdates(advert, request);

        if (updatedCount == 0)
            return Result<AdvertisementUpdateResponse>.Fail(
                "No fields to update");

        await _repository.UpdateAsync(advert);

        return Result<AdvertisementUpdateResponse>.Success(
            new AdvertisementUpdateResponse(
                DateTime.UtcNow,
                updatedCount
            )
        );
    }

    private static int ApplyUpdates(
        Advertisement advert,
        AdvertisementUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.Title is not null)
        {
            advert.Title = request.Title;
            countUpdated++;
        }

        if (request.Description is not null)
        {
            advert.Description = request.Description;
            countUpdated++;
        }

        if (request.Price is not null)
        {
            var newPrice = request.Price.Value;

            if (newPrice < advert.Price)
                advert.PreviousPrice = advert.Price;
            else
                advert.PreviousPrice = newPrice;

            advert.Price = newPrice;
            countUpdated++;
        }

        if (request.Popularity is not null)
        {
            advert.Popularity = request.Popularity.Value;
            countUpdated++;
        }

        if (request.CurrencyId is not null)
        {
            advert.CurrencyId = request.CurrencyId.Value;
            countUpdated++;
        }

        if (request.CategoryId is not null)
        {
            advert.CategoryId = request.CategoryId.Value;
            countUpdated++;
        }

        if (request.Properties is not null)
        {
            advert.Properties = request.Properties;
            countUpdated++;
        }

        return countUpdated;
    }
}