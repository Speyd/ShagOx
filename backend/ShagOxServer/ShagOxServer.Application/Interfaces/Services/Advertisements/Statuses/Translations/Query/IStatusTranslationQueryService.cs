using ShagOxServer.Application.DTOs.Advertisements.Statuses.Translations;
using ShagOxServer.Application.Interfaces.Services.Base.Translations;
using ShagOxServer.Domain.Filters.Advertisements.Translations;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Translations.Query;
public interface IStatusTranslationQueryService
    : IQueryTranslationService<StatusTranslationDto,
        StatusTranslationSearchFilter>
{
}