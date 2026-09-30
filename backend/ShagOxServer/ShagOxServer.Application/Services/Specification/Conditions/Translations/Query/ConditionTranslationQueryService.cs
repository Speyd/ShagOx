using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Specification.Conditions.Translations;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Query;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Specification.Conditions.Translations.Mapping;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Entities.Specification.Translations;
using ShagOxServer.Domain.Filters.Specification.Conditions.Translations;

namespace ShagOxServer.Application.Services.Specification.Conditions.Translations.Query;
public class ConditionTranslationQueryService
    : BaseTranslationQueryService<
        ConditionTranslationDto,
        Condition,
        ConditionTranslation,
        ConditionTranslationSearchFilter
        >,
    IConditionTranslationQueryService
{
    public ConditionTranslationQueryService(
        IConditionTranslationQueryRepository condtitionRepository,
        ICacheService cacheService,
        IOptions<CacheSettings> settings
    )
        : base(condtitionRepository, cacheService, settings)
    {
    }

    public override async Task<ConditionTranslationDto> ApplyMapperAsync(
        ConditionTranslation entity)
    {
        return ConditionTranslationMapper.ToDto(entity);
    }
}