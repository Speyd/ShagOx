using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Providers.User;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserSessions.Query;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Services.Auth.Core;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions;
using ShagOxServer.Domain.Entities.Auth.Users;
using ShagOxServer.Domain.Entities.Auth.Users.Enum;

namespace ShagOxServer.Application.Services.Auth.Core.Logout;
public class LogoutService
    : ILogoutService
{
    private readonly IRepository<UserSession> _sessionRepository;
    private readonly ISessionProvider _sessionProvider;
    private readonly IRefreshTokenService _refreshTokenProvider;
    private readonly IUserSessionQueryRepository _sessionQueryRepository;
    private readonly IUnitOfWork _unitOfWork;


    public LogoutService(
        IRepository<UserSession> sessionRepository,
        ISessionProvider sessionProvider,
        IRefreshTokenService refreshTokenProvider,
        IUserSessionQueryRepository sessionQueryRepository,
        IUnitOfWork unitOfWork)
    {
        _sessionRepository = sessionRepository;
        _refreshTokenProvider = refreshTokenProvider;
        _sessionProvider = sessionProvider;
        _sessionQueryRepository = sessionQueryRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task LogoutAsync(
        string refreshToken,
        CancellationToken cancellationToken)
    {
        var session = await _sessionQueryRepository
            .GetByRefreshTokenHashAsync(
                _refreshTokenProvider.Hash(refreshToken), 
                cancellationToken);

        if (session is null)
            return;

        session.RevokedAt = DateTime.UtcNow;
        session.RevocationReason = SessionRevocationReason.Logout;

        _sessionRepository.Update(session);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task LogoutAllAsync(
        long userId,
        CancellationToken cancellationToken)
    {
        var sessions = await _sessionQueryRepository
            .GetAllActiveByUserAsync(
                userId,
                cancellationToken);

        foreach(var session in sessions)
        {
            session.RevokedAt = DateTime.UtcNow;
            session.RevocationReason = SessionRevocationReason.Logout;

            _sessionRepository.Update(session);
        }

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task LogoutOtherSessions(
        long userId,
        CancellationToken cancellationToken)
    {
        var sessions = await _sessionQueryRepository
            .GetAllActiveByUserAsync(
                userId,
                cancellationToken);

        var userSession = await _sessionProvider
            .GetSessionAsync();

        foreach (var session in sessions)
        {
            if (userSession?.Id == session.Id)
                continue;

            session.RevokedAt = DateTime.UtcNow;
            session.RevocationReason = SessionRevocationReason.Logout;

            _sessionRepository.Update(session);
        }

        await _unitOfWork.SaveChangesAsync();
    }
}
