using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices;
using ShagOxServer.Application.Interfaces.Services.Verifications.Confirmation;
using ShagOxServer.Application.Resources.Verifications.Core;
using ShagOxServer.Domain.Entities.Auth.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Verifications;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Verifications.Confirmation;
public class UserVerificationService
    : IUserVerificationService
{
    private readonly IDeviceProvider _deviceProvider;
    private readonly IUserDeviceCommandRepository _deviceCommandRepository;
    private readonly IUserDeviceQueryRepository _deviceQueryRepository;

    public UserVerificationService(
        IDeviceProvider deviceProvider,
        IUserDeviceCommandRepository deviceCommandRepository,
        IUserDeviceQueryRepository deviceQueryRepository)
    {
        _deviceProvider = deviceProvider;
        _deviceCommandRepository = deviceCommandRepository;
        _deviceQueryRepository = deviceQueryRepository;
    }

    public async Task<Result<bool>> ConfirmEmail(
       User user,
       VerificationCode verificationCode)
    {
        if (user.EmailConfirmed)
        {
            return Result<bool>.Fail(
                VerificationResources.EmailAlreadyConfirmed);
        }

        user.Email =
            verificationCode.PendingValue
            ?? user.Email;

        user.EmailConfirmed = true;
        user.Status = UserStatus.Active;

        await TrastDevice(user);

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ChangeEmail(
        User user,
        VerificationCode verificationCode)
    {
        if (string.IsNullOrWhiteSpace(
                   verificationCode.PendingValue))
        {
            return Result<bool>.Fail(
                VerificationResources.PendingEmailNotFound);
        }

        user.Email = verificationCode.PendingValue;
        user.EmailConfirmed = true;
        user.Status = UserStatus.Active;

        await TrastDevice(user);

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ConfirmPhone(
        User user,
        VerificationCode verificationCode)
    {
        if (user.PhoneConfirmed)
        {
            return Result<bool>.Fail(
                VerificationResources.PhoneAlreadyConfirmed);
        }

        user.Phone =
            verificationCode.PendingValue
            ?? user.Phone;

        user.PhoneConfirmed = true;
        user.Status = UserStatus.Active;

        await TrastDevice(user);

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ChangePhone(
        User user,
        VerificationCode verificationCode)
    {

        if (string.IsNullOrWhiteSpace(
                  verificationCode.PendingValue))
        {
            return Result<bool>.Fail(
                VerificationResources.PendingPhoneNotFound);
        }

        user.Phone = verificationCode.PendingValue;
        user.PhoneConfirmed = true;
        user.Status = UserStatus.Active;

        await TrastDevice(user);

        return Result<bool>.Success(true);
    }

    public async Task<Result<bool>> ChangePassword(
        User user,
        VerificationCode verificationCode)
    {
        if (string.IsNullOrWhiteSpace(
                  verificationCode.PendingValue))
        {
            return Result<bool>.Fail(
                VerificationResources.PendingPasswordNotFound);
        }

        user.PasswordHash =
            verificationCode.PendingValue
            ?? user.PasswordHash;

        await TrastDevice(user);

        return Result<bool>.Success(true);
    }

    private async Task TrastDevice(
        User user)
    {
        var usedDevice = await _deviceProvider
            .GetDeviceAsync();

        if (usedDevice is not null)
        {
            await _deviceCommandRepository
                .MarkAsTrasteAsync(usedDevice.Id);
        }

        var userDevices = await _deviceQueryRepository
            .GetAllByUserAsync(user.Id);

        foreach(var device in userDevices)
        {
            if (device.Id == usedDevice?.Id)
                continue;

            await _deviceCommandRepository
                .UnmarkAsTrasteAsync(device.Id);
        }
    }
}
