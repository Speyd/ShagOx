using ShagOxServer.Application.DTOs.Specification.Conditions.Translations;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions.Translations;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Query;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Specification.Conditions.Translations.Mapping;
using ShagOxServer.Domain.Entities.Specification.Translations;
using ShagOxServer.Domain.Filters.Specification.Conditions.Translations;

namespace ShagOxServer.Application.Services.Specification.Conditions.Translations.Query;
public class ConditionTranslationQueryService
    : BaseTranslationQueryService<
        ConditionTranslationDto,
        ConditionTranslation,
        ConditionTranslationSearchFilter
        >,
    IConditionTranslationQueryService
{
    public ConditionTranslationQueryService(
        IConditionTranslationQueryRepository condtitionRepository
    )
        : base(condtitionRepository)
    {
    }

    protected override ConditionTranslationDto ApplyMapper(
        ConditionTranslation entity)
    {
        return ConditionTranslationMapper.ToDto(entity);
    }
}