using Google.Apis.Auth;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.Interfaces.Services.Auth.Externals;
using ShagOxServer.Application.Resources.Auth.External.Google;
using ShagOxServer.Application.Resources.Auth.Logins;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Login.Externals;
public partial class GoogleLoginService
    : IGoogleLoginService
{
    private async Task<Result<LoginResponse>> AuthenticateAsync(
        GoogleJsonWebSignature.Payload payload)
    {
        if (!payload.EmailVerified ||
            string.IsNullOrWhiteSpace(payload.Email))
        {
            return Result<LoginResponse>.Fail(
                GoogleAuthResources.EmailNotVerified);
        }

        var user = await _userQueryRepository.GetByEmailAsync(
            payload.Email);

        if (user is null)
        {
            var createResult = await CreateGoogleUserAsync(payload);

            if (!createResult.IsSuccess || createResult.Value is null)
            {
                return Result<LoginResponse>.Fail(
                    createResult.Error);
            }

            user = createResult.Value;
        }

        if (!_loginPolicy.CanLogin(user))
        {
            return Result<LoginResponse>.Fail(
                LoginAuthResources.LoginNotAllowed);
        }

        var device = await _deviceProvider
            .GetDeviceAsync();

        if(device is null)
        {
            return Result<LoginResponse>.Fail(
                EntityNamesResources.UserDevice);
        }

        if (!device.IsTrusted)
        {
            await _deviceCommands.MarkAsTrasteAsync(device.Id);
        }

        return await _sessionService
            .CreateSessionAsync(user, device.Id);
    }


    private async Task<Result<User>> CreateGoogleUserAsync(
        GoogleJsonWebSignature.Payload payload)
    {
        var userName = await _userNameService.GenerateUniqueAsync(
            payload.Name ?? payload.Email);

        if (!userName.IsSuccess)
        {
            return Result<User>.Fail(userName.Error);
        }

        var user = new User
        {
            Email = payload.Email,
            UserName = userName.Value!,
            EmailConfirmed = true,
            Status = UserStatus.Active
        };

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            await _register.CreateUserAggregateAsync(user);

            await _userCreater.SetDefaultName(user);

            await _unitOfWork.CommitAsync();

            return Result<User>.Success(user);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Google user creation failed.");

            return Result<User>.Fail(
                GoogleAuthResources.GoogleUserCreationFailed);
        }
    }

}
