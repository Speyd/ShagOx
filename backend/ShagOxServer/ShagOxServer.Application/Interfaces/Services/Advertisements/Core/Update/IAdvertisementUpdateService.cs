using ShagOxServer.Application.DTOs.Advertisements.Update;
using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Core.Update;
public interface IAdvertisementUpdateService
    : IUpdateService<UpdateResponse, AdvertisementUpdateRequest>
{
}