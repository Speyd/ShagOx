using ShagOxServer.Application.DTOs.Advertisements.Core.Update;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Images;
public interface IAdvertisementImageService
{
    Task<Result<bool>> SyncImagesAsync(
       long advertisementId,
       AdvertisementUpdateRequest request);

    Task<Result<bool>> SyncImagesAsync(
        Advertisement advertisement,
        AdvertisementUpdateRequest request);
}