using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.DTOs.Advertisements.Delete;

namespace ShagOxServer.Application.Interfaces.Advertisements.Delete;
public interface IAdvertisementDeleteyService
{
    Task<Result<AdvertisementDeleteResponse>> DeleteAdvertisementAsync(
        int id);
}
