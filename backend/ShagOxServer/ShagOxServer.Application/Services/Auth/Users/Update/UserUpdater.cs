using ShagOxServer.Application.DTOs.Auth.Users.Update;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Auth.Users.Update;
public static class UserUpdater
{
    public static int ApplyUpdates(
        User user,
        UserUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.Surname is not null)
        {
            user.Surname = request.Surname;
            countUpdated++;
        }

        if (request.Name is not null)
        {
            user.Name = request.Name;
            countUpdated++;
        }

        if (request.Phone is not null)
        {
            user.Phone = request.Phone;
            countUpdated++;
        }

        if (request.Email is not null)
        {
            user.Email = request.Email;
            countUpdated++;
        }

        if (request.Avatar is not null)
        {
            user.Avatar = request.Avatar;
            countUpdated++;
        }

        if (request.CityId is not null)
        {
            user.CityId = request.CityId.Value;
            countUpdated++;
        }

        return countUpdated;
    }
}