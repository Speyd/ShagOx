using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Location.Regions.Update;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Location.Regions.Update;
public interface IRegionUpdateService
    : IUpdateService<UpdateResponse, RegionUpdateRequest>
{
}