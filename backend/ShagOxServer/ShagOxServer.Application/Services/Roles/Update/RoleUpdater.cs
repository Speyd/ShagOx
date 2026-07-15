using ShagOxServer.Application.DTOs.Roles.Update;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Roles.Update;
public static class RoleUpdater
{
    public static int ApplyUpdates(
        Role role,
        RoleUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.Name is not null)
        {
            role.Name = request.Name;
            countUpdated++;
        }

        if (request.Description is not null)
        {
            role.Description = request.Description;
            countUpdated++;
        }

        return countUpdated;
    }
}