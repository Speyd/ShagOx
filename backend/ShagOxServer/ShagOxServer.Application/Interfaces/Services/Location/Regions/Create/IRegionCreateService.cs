using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Location.Regions.Create;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Location.Regions.Create;
public interface IRegionCreateService
    : ICreateService<
        CreateResponse,
        RegionCreateRequest
        >
{
}