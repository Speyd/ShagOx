using ShagOxServer.Application.DTOs.Advertisements.Statuses.Create;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Create;

public static class StatusCreater
{
    public static Status Create(
       StatusCreateRequest request)
    {
        return new Status
        {
            Code = request.Code
        };
    }
}