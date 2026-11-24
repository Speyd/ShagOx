using ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Create;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base.Create;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices.Create;
public interface IUserDeviceCreateService
    : ICreateService<
        CreateResponse,
        UserDeviceCreateRequest
        >
{
    Task<Result<UserDevice>> CreateInternalAsync(
        UserDeviceCreateRequest request,
        CancellationToken cancellationToken = default);
}
