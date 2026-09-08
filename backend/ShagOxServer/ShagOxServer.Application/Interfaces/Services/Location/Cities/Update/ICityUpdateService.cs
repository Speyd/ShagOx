using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Location.Cities.Update;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Location.Cities.Update;
public interface ICityUpdateService
    : IUpdateService<UpdateResponse, CityUpdateRequest>
{
}