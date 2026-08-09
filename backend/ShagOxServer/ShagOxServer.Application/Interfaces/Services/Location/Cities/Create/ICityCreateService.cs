using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.DTOs.Location.Cities.Create;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Location.Cities.Create;
public interface ICityCreateService
    : ICreateService<
        CreateResponse,
        CityCreateRequest
        >
{
}