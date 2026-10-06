using ShagOxServer.Application.DTOs.Auth.Register;
using ShagOxServer.Application.Interfaces.Services.Auth;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Account.Enum;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Register;
public partial class RegisterService
    : IRegisterService
{
    private async Task<Result<(User User, bool IsExisting)>>
        PrepareUserForRegistrationAsync(
            RegisterRequest request)
    {
        bool isExisting = false;
        User? user = null;

        var userGet = await _userQueryRepository
            .GetByContactAsync(request.EmailOrPhone);

        if (userGet is not null)
        {
            if (userGet.Status != UserStatus.PendingVerification)
            {
                return Result<(User, bool)>
                    .AlreadyExists(EntityNamesResources.User);
            }

            user = userGet;

            var result = await _contactApplier
                .ApplyAsync(user, request);

            if (!result.IsSuccess)

            {
                return Result<(User User, bool IsExisting)>
                    .Fail(result.Error);
            }

            isExisting = true;
        }
        else
        {
            var userResult = await _userCreater
                .CreateUser(request);

            if (!userResult.IsSuccess)

            {
                return Result<(User User, bool IsExisting)>
                    .Fail(userResult.Error);
            }

            user = userResult.Value;
        }

        var passResult = _passwordService
            .Apply(user!, request);

        if (!passResult.IsSuccess)

        {
            return Result<(User User, bool IsExisting)>
                .Fail(passResult.Error);
        }

        return Result<(User, bool)>
            .Success((user!, isExisting));
    }
}