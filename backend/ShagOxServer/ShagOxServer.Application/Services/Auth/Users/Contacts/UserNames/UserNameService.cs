using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Auth;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts.UserNames;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Contacts.UserNames;

public class UserNameService
    : IUserNameService
{
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly UserNameSettings _userNameSettings;

    public UserNameService(
        IUserQueryRepository userQueryRepository,
        IOptions<UserNameSettings> userNameSettings)
    {
        _userQueryRepository = userQueryRepository;

        _userNameSettings = userNameSettings.Value;
    }


    public async Task<Result<string>> GenerateUniqueAsync(
        string userName)
    {
        try
        {
            var baseUserName = userName.Trim();
            var candidate = baseUserName;
            var maxAttempts = _userNameSettings.MaxAttempts;

            for (var attempt = 0; attempt < maxAttempts; attempt++)
            {
                if (await _userQueryRepository
                    .GetByUserNameAsync(candidate) is null)
                {
                    return Result<string>
                        .Success(candidate);
                }

                candidate = $"{baseUserName}{attempt + 1}";
            }


            return Result<string>.Success(candidate);
        }
        catch
        {
            return Result<string>
                .Fail("Failed to generate a unique username.");
        }
    }
}
