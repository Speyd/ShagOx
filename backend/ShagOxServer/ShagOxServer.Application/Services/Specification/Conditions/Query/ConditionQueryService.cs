using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Specification.Conditions.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Query;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Specification.Conditions.Mapping;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Filters.Specification.Conditions;

namespace ShagOxServer.Application.Services.Specification.Conditions.Query;
public class ConditionQueryService 
    : BaseTranslatableQueryService<
        ConditionDto,
        Condition,
        ConditionSearchFilter
        >,
    IConditionQueryService
{
    private readonly IConditionTranslationQueryRepository _translationRepository;


    public ConditionQueryService(
        IConditionQueryRepository conditionQueryRepository,
        IConditionTranslationQueryRepository translationRepository,
        ILanguageProvider language,
        ICacheService cacheService,
        IOptions<CacheSettings> settings
    )
        : base(conditionQueryRepository, language, cacheService, settings)
    {
        _translationRepository = translationRepository;
    }


    public override async Task<ConditionDto> ApplyMapperAsync(
        Condition entity)
    {
        var translation = await _translationRepository
            .GetByIdentificatorAsync(entity.Code, _language.Language);

        return ConditionMapper.ToDto(entity, translation?.Name);
    }
}