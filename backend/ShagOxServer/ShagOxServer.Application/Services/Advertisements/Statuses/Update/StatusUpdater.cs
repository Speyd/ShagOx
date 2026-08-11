using ShagOxServer.Application.DTOs.Advertisements.Statuses.Update;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Update;
public class StatusUpdater
{
    public static int ApplyUpdates(
        Status status,
        StatusUpdateRequest request)
    {
        int countUpdated = 0;

        if (request.Code is not null)
        {
            status.Code = request.Code;
            countUpdated++;
        }

        if (request.Name is not null)
        {
            status.Name = request.Name;
            countUpdated++;
        }

        if (request.Description is not null)
        {
            status.Description = request.Description;
            countUpdated++;
        }

        return countUpdated;
    }
}