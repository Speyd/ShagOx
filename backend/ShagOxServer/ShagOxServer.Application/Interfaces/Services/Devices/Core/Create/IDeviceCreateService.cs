using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.DTOs.Devices.Create;
using ShagOxServer.Application.Interfaces.Services.Base.Create;

namespace ShagOxServer.Application.Interfaces.Services.Devices.Core.Create;
public interface IDeviceCreateService
    : ICreateService<
        CreateResponse,
        DeviceCreateRequest
        >
{
}
