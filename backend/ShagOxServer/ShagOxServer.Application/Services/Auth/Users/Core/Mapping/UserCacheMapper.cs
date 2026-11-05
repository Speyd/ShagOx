using ShagOxServer.Application.DTOs.Auth.Users.Core.Cache;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Auth.Users.Core.Mapping;
public static class UserCacheMapper
{
    public static UserCacheInfo ToInfo(
        User user,
        EntityStatus status = EntityStatus.Active)
    {
        return new UserCacheInfo(
            user.Id,
            user.Phone,
            user.Email, 
            user.UserName,
            status
        );
    }
}
