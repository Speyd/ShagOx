using ShagOxServer.Application.DTOs.Advertisements.Core.Create;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Create;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Create;
public interface IAdvertisementCreateService
    : ICreateOwnerService<
        CreateResponse,
        AdvertisementCreateRequest
        >
{
}
