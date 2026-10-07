using Google.Apis.Auth;
using Microsoft.Extensions.Logging;
using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.Interfaces.Services.Auth.Externals;
using ShagOxServer.Application.Resources.Auth.External.Google;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Account.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Externals;
public partial class GoogleLoginService
    : IGoogleLoginService
{
    public async Task<Result<LoginResponse>> AuthenticateAsync(
         GoogleJsonWebSignature.Payload payload)
    {
        try
        {
            if (payload.EmailVerified != true)
            {
                return Result<LoginResponse>.Unauthorized(
                    GoogleAuthResources.GoogleAuthenticationFailed);
            }

            var email = payload.Email?.Trim();
            if (string.IsNullOrWhiteSpace(email))
            {
                return Result<LoginResponse>.Unauthorized(
                    GoogleAuthResources.GoogleAuthenticationFailed);
            }


            var user = await _userQueryRepository
                .GetByEmailAsync(email);

            if (user is null)
            {
                var name = string.IsNullOrWhiteSpace(payload.Name)
                    ? email.Split('@')[0]
                    : payload.Name.Trim();
                var userName = await _userNameService
                    .GenerateUniqueAsync(name);
                if (!userName.IsSuccess)
                    return Result<LoginResponse>.Fail(userName.Error);

                user = new User
                {
                    Email = email,
                    EmailConfirmed = true,
                    UserName = userName.Value!,
                    Status = UserStatus.Active
                };

                await _unitOfWork.BeginTransactionAsync();

                try
                {
                    _userRepository.Add(user);

                    var roleResult = await _roleService
                        .AddDefaultRoleAsync(user);

                    if (!roleResult.IsSuccess)
                    {
                        await _unitOfWork.RollbackAsync();
                        return Result<LoginResponse>.Fail(roleResult.Error);
                    }

                    await _unitOfWork.SaveChangesAsync();

                    await _userCreater.SetDefaultName(user);

                    await _unitOfWork.CommitAsync();
                }
                catch(Exception ex)
                {
                    await _unitOfWork.RollbackAsync();

                    _logger.LogError(
                        ex,
                        "Failed to authenticate user with Google. Email: {Email}",
                        payload.Email);

                    return Result<LoginResponse>
                        .Fail(GoogleAuthResources.GoogleUserCreationFailed);
                }
            }
            else if (!user.EmailConfirmed)
            {
                var writableUser = await _userRepository
                    .GetByIdAsync(user.Id);

                if (writableUser is not null)
                {
                    writableUser.EmailConfirmed = true;
                    await _unitOfWork.SaveChangesAsync();
                }
            }

            return Result<LoginResponse>.Success(
                new LoginResponse(_jwtService.GenerateToken(user))
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to authenticate user with Google. Email{Email}",
                payload.Email);

            return Result<LoginResponse>.Fail(
                GoogleAuthResources.GoogleAuthenticationFailed);
        }
    }
}
