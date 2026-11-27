using ShagOxServer.Application.DTOs.Auth.Users.UserDevices.Create;
using ShagOxServer.Application.Interfaces.Services.Auth;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Register;
public partial class RegisterService
    : IRegisterService
{
    private async Task<Result<bool>> EnsureUserDeviceAsync(
        long userId,
        long deviceid)
    {
        var userDevice = await _userQueryDeviceService
            .GetByUserAndDeviceAsync(userId, deviceid);

        if (userDevice is not null)
            return Result<bool>.Success(true);

        var deviceResult = await _userDeviceService.CreateInternalAsync(
            new UserDeviceCreateRequest(userId, deviceid));

        if (!deviceResult.IsSuccess)
        {
            await _unitOfWork.RollbackAsync();

            return Result<bool>
                .Fail(deviceResult.Error);
        }

        return Result<bool>.Success(true);
    }
}
