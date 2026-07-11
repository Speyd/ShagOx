using ShagOxServer.Application.DTOs.Advertisements.Update;
using ShagOxServer.Application.DTOs.Advertisements.Update.Images;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Images;
public interface IAdvertisementImageService
{
    Task<Result<bool>> UpdateImagesOrderAsync(
        int advertisementId,
       ImageOrderUpdateRequest request);

    Task<Result<bool>> SyncImagesAsync(
       int advertisementId,
       AdvertisementUpdateRequest request);

    Task<Result<bool>> SyncImagesAsync(
        Advertisement advertisement,
        AdvertisementUpdateRequest request);

    Task RecalculateImagesOrderAsync(
        int advertisementId);
}