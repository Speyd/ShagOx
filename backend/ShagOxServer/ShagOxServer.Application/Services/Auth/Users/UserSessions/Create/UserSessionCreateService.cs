using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Auth;
using ShagOxServer.Application.Common.Options.Systems.Cookies;
using ShagOxServer.Application.DTOs.Auth.Users.UserSessions.Create;
using ShagOxServer.Application.DTOs.Auth.Users.UserSessions.Create.Response;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Providers.Sessions;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions.Create;
using ShagOxServer.Application.Resources.EntityErrors;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Application.Services.Auth.Users.UserDevices.Validator;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.UserSessions.Create;
public class UserSessionCreateService
    : IUserSessionCreateService
{
    private readonly IRepository<UserSession> _sessionRepository;
    private readonly UserDeviceValidator _deviceValidator;
    private readonly UserValidator _userValidator;

    private readonly ISessionInfoProvider _sessionInfoProvider;
    private readonly IRefreshTokenService _toketService;
    private readonly RefreshTokenOptions _refreshTokenOption;

    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserSessionCreateService> _logger;


    public UserSessionCreateService(
        IRepository<UserSession> sessionRepository,
        UserDeviceValidator deviceValidator,
        UserValidator userValidator,
        ISessionInfoProvider sessionInfoProvider,
        IRefreshTokenService toketService,
        IOptions<RefreshTokenOptions> refreshTokenOption,
        IUnitOfWork unitOfWork,
        ILogger<UserSessionCreateService> logger)
    {
        _sessionRepository = sessionRepository;
        _deviceValidator = deviceValidator;
        _userValidator = userValidator;
        _sessionInfoProvider = sessionInfoProvider;
        _toketService = toketService;
        _refreshTokenOption = refreshTokenOption.Value;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<UserSessionCreateResponse>> CreateAsync(
        UserSessionCreateRequest request,
        CancellationToken cancellationToken = default)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            string token = _toketService.Generate();

            var sessionResult = await CreateInternalAsync(request, token);

            if (!sessionResult.IsSuccess ||
                sessionResult.Value is null)
            {
                return Result<UserSessionCreateResponse>
                    .Fail(sessionResult.Error);
            }

            await _unitOfWork.CommitAsync();

            return Result<UserSessionCreateResponse>.Success(
               new UserSessionCreateResponse(
                   sessionResult.Value.Id,
                   DateTime.UtcNow,
                   token,
                   sessionResult.Value.ExpiresAt
            ));
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();

            _logger.LogError(
                ex,
                "Failed to create session. UserId: {UserId}",
                request.OwnerId);

            return Result<UserSessionCreateResponse>
                .Fail(EntityErrorResources.UserSessionCreateFailed);
        }
    }

    public async Task<Result<UserSession>> CreateInternalAsync(
        UserSessionCreateRequest request,
        string token,
        CancellationToken cancellationToken = default)
    {

        var tokenHash = _toketService.Hash(token);
        
        var userValidation = await _userValidator
           .ExistsByIdAsync(request.OwnerId);

        if (!userValidation.IsSuccess)
            return Result<UserSession>.Fail(userValidation.Error);

        var deviceValidation = await _deviceValidator
           .ExistsAsync(request.OwnerId, request.UserDeviceId);

        if (!deviceValidation.IsSuccess)
            return Result<UserSession>.Fail(deviceValidation.Error);

        var info = _sessionInfoProvider.GetSessionInfo();

        var session = UserSessionCreater.Create(
            request,
            info,
            _refreshTokenOption.ExpireDays,
            tokenHash);

        _sessionRepository.Add(session);

        return Result<UserSession>.Success(session);
    }
}
