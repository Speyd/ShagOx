using ShagOxServer.Application.DTOs.Advertisements.Statuses.Update;
using ShagOxServer.Application.DTOs.Base.Responses;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Update;
public interface IStatusUpdateService
    : IUpdateService<UpdateResponse, StatusUpdateRequest>
{
}