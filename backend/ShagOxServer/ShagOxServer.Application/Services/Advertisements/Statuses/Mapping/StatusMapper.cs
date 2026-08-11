using ShagOxServer.Application.DTOs.Advertisements.Statuses;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Mapping;
public static class StatusMapper
{
    public static StatusDto ToDto(
        Status x)
    {
        return new StatusDto
        (
            x.Id,
            x.Code,
            x.Name,
            x.Description
        );
    }
}