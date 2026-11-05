using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Application.DTOs.Specification.Currencies.Query;
using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Query;
using ShagOxServer.Application.Services.Specification.Currencies.Mapping;
using ShagOxServer.Application.Services.Specification.Pictures.Avatars.Mapping;
using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Auth.Users.Core.Mapping;
public static class UserShortMapper
{
    public static UserShortDto ToDto(
        User user)
    {
        return new UserShortDto(
            user.Id,
            user.EntityStatus,
            user.FirstName,
            user.LastName,
            user.UserName,
            user.Bio,
            user.Phone,
            user.Email,
            MapCurrency(user.Currency),
            MapAvatar(user.Avatar),
            user.CityId,
            user.LastSeenAt
        );
    }

    private static AvatarDto? MapAvatar(
        Avatar? avatar)
    {
        return avatar is null
            ? null
            : AvatarMapper.ToDto(avatar);
    }

    private static CurrencyDto? MapCurrency(
        Currency? currency)
    {
        return currency is null
            ? null
            : CurrencyMapper.ToDto(currency);
    }
}
