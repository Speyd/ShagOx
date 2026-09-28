using ShagOxServer.Application.DTOs.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions.Translations;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Query;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Specification.Conditions.Mapping;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Filters.Specification.Conditions;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Specification.Conditions.Query;
public class ConditionQueryService 
    : BaseQueryService<
        ConditionDto,
        Condition,
        ConditionSearchFilter
        >,
    IConditionQueryService
{
    private readonly IConditionQueryRepository _conditionQueryRepository;

    private readonly IConditionTranslationQueryRepository _translationRepository;

    private readonly ILanguageProvider _language;


    public ConditionQueryService(
        IConditionQueryRepository conditionQueryRepository,
        IConditionTranslationQueryRepository translationRepository,
        ILanguageProvider language
    )
        : base(conditionQueryRepository)
    {
        _conditionQueryRepository = conditionQueryRepository;
        _translationRepository = translationRepository;
        _language = language;
    }


    public override async Task<ConditionDto> ApplyMapperAsync(
        Condition entity)
    {
        var translation = await _translationRepository
            .GetByIdentificatorAsync(entity.Code, _language.Language);

        return ConditionMapper.ToDto(entity, translation?.Name);
    }

    public async Task<Result<ConditionDto>> GetByCodeAsync(
        string code)
    {
        var condition = await _conditionQueryRepository
            .GetByCodeAsync(code);

        return await condition.ToResultAsync(ApplyMapperAsync);
    }
}