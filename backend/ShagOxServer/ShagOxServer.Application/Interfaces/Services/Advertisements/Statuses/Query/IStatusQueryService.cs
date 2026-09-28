using ShagOxServer.Application.DTOs.Advertisements.Statuses;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Query;
public interface IStatusQueryService
    : IQueryService<StatusDto, Status, StatusSearchFilter>
{
}