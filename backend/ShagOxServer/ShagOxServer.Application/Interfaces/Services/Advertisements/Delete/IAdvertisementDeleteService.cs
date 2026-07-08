using ShagOxServer.Application.DTOs.Advertisements.Delete;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Delete;
public interface IAdvertisementDeleteService
{
    Task<Result<AdvertisementDeleteResponse>> DeleteAdvertisementAsync(
        int id);
}
