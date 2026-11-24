using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Auth;
using ShagOxServer.Application.Common.Options.Systems.Cookies;
using ShagOxServer.Application.DTOs.Auth.Login;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserSessions;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Auth.Tokens.Jwt;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions.Refresh;
using ShagOxServer.Application.Resources.EntityNames;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Auth.Users.UserSessions.Refresh;
public class UserSessionRefreshService
    : IUserSessionRefreshService
{
    private readonly IRepository<UserSession> _sessionRepository;
    private readonly IUserSessionQueryRepository _sessionQueryRepository;
    private readonly RefreshTokenOptions _refreshTokenOption;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IJwtService _jwtService;
    private readonly IUnitOfWork _unitOfWork;


    public UserSessionRefreshService(
        IRepository<UserSession> sessionRepository,
        IUserSessionQueryRepository sessionQueryRepository,
        IOptions<RefreshTokenOptions> refreshTokenOption,
        IRefreshTokenService refreshTokenService,
        IJwtService jwtService,
        IUnitOfWork unitOfWork)
    {
        _sessionRepository = sessionRepository;
        _sessionQueryRepository = sessionQueryRepository;
        _refreshTokenOption = refreshTokenOption.Value;
        _refreshTokenService = refreshTokenService;
        _jwtService = jwtService;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<LoginResponse>> RefreshAsync(
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        var tokenHash = _refreshTokenService.Hash(refreshToken);

        var now = DateTime.UtcNow;

        var session = await _sessionQueryRepository
            .GetByRefreshTokenHashAsync(
                tokenHash,
                cancellationToken);

        if (session is null)
        {
            return Result<LoginResponse>.NotFound(
                EntityNamesResources.UserSession);
        }

        if (session.RevokedAt != null)
        {
            return Result<LoginResponse>.Fail(
                "Session has been revoked.");
        }

        if (session.ExpiresAt <= now)
        {
            return Result<LoginResponse>.Fail(
                "Refresh token expired.");
        }


        var newRefreshToken = _refreshTokenService.Generate();

        session.RefreshTokenHash =
            _refreshTokenService.Hash(newRefreshToken);

        session.LastUsedAt = now;
        session.ExpiresAt = now.AddDays(
            _refreshTokenOption.ExpireDays);

        _sessionRepository.Update(session);

        await _unitOfWork.SaveChangesAsync();


        var accessToken = _jwtService.GenerateToken(
            session.Owner,
            session.Id);

        return Result<LoginResponse>.Success(
            new LoginResponse(
                accessToken,
                newRefreshToken));
    }
}
