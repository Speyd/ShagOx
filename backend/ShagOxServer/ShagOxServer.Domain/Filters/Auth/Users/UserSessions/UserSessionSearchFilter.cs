using ShagOxServer.Domain.Entities.Auth.Users.Enum;

namespace ShagOxServer.Domain.Filters.Auth.Users.UserSessions;
public sealed record UserSessionSearchFilter
(
    long? UserId,
    long? UserDeviceId,
    SessionRevocationReason? RevocationReason,
    bool? IsActive
) : BaseFilter();
