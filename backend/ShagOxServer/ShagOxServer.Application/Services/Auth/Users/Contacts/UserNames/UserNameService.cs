using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Auth;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Contacts.UserNames;
using ShagOxServer.Application.Resources.Auth.Contacts.UserNames;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.Contacts.UserNames;
public class UserNameService
    : IUserNameService
{
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly UserNameSettings _userNameSettings;

    private readonly ILogger<UserNameService> _logger;

    public UserNameService(
        IUserQueryRepository userQueryRepository,
        IOptions<UserNameSettings> userNameSettings,
        ILogger<UserNameService> logger)
    {
        _userQueryRepository = userQueryRepository;
        _userNameSettings = userNameSettings.Value;
        _logger = logger;
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
        catch(Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to generate a unique username. UserName: {userName}",
                userName);

            return Result<string>.Fail(
                UserNameAuthResources.FailedToGenerateUniqueUsername);
        }
    }
}
