using ShagOxServer.Application.DTOs.Specification.Conditions.Translations;
using ShagOxServer.Application.Interfaces.Services.Base.Translations;
using ShagOxServer.Domain.Filters.Specification.Conditions.Translations;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Query;
public interface IConditionTranslationQueryService
    : ITranslationQueryService<ConditionTranslationDto, 
        ConditionTranslationSearchFilter>
{
}