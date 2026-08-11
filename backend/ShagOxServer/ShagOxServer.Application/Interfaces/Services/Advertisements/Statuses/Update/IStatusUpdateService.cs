using ShagOxServer.Application.DTOs.Advertisements.Statuses.Update;
using ShagOxServer.Application.DTOs.Common.Responses;
using ShagOxServer.Application.Interfaces.Services.Base;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Update;
public interface IStatusUpdateService
    : IUpdateService<UpdateResponse, StatusUpdateRequest>
{
}