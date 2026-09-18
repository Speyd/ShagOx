using ShagOxServer.Application.DTOs.Auth.Users.Core.Update;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Auth.Users.Update;
public static class UserUpdater
{
    public static int ApplyUpdates(
        User user,
        UserUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.FirstName is not null)
        {
            user.FirstName = request.FirstName;
            countUpdated++;
        }

        if (request.LastName is not null)
        {
            user.LastName = request.LastName;
            countUpdated++;
        }

        if (request.UserName is not null)
        {
            user.UserName = request.UserName;
            countUpdated++;
        }

        if (request.Bio is not null)
        {
            user.Bio = request.Bio;
            countUpdated++;
        }

        if (request.Avatar is not null)
        {
            countUpdated++;
        }

        if (request.CityId.HasValue)
        {
            user.CityId = request.CityId.Value;
            countUpdated++;
        }

        return countUpdated;
    }
}