using ShagOxServer.Application.DTOs.Advertisements.Statuses.Query;
using ShagOxServer.Application.Interfaces.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Query;
public interface IStatusQueryService
    : ITranslatableQueryService<StatusDto,
        Status, 
        StatusSearchFilter>
{
}