using Google.Apis.Auth;
using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.Interfaces.Services.Auth.Externals;
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
            string email = payload.Email;
            string name = payload.Name;


            var user = await _userQueryRepository.GetByEmailAsync(email);

            if (user is null)
            {
                var userName = await _userNameService
                    .GenerateUniqueAsync(name);
                if (!userName.IsSuccess)
                    return Result<LoginResponse>.Fail(userName.Error);

                user = new User
                {
                    Email = email,
                    UserName = userName.Value!,
                    Status = UserStatus.Active
                };

                await _unitOfWork.BeginTransactionAsync();

                try
                {
                    _userRepository.Add(user);

                    var roleResult = await _roleService.AddDefaultRoleAsync(user);
                    if (!roleResult.IsSuccess)
                    {
                        await _unitOfWork.RollbackAsync();
                        return Result<LoginResponse>.Fail(roleResult.Error);
                    }

                    await _unitOfWork.SaveChangesAsync();

                    await _userCreater.SetDefaultName(user);

                    await _unitOfWork.CommitAsync();
                }
                catch
                {
                    await _unitOfWork.RollbackAsync();
                    throw;
                }
            }

            return Result<LoginResponse>.Success(
                new LoginResponse(_jwtService.GenerateToken(user))
            );
        }
        catch (Exception ex)
        {
            return Result<LoginResponse>.Fail(ex.Message);
        }
    }
}