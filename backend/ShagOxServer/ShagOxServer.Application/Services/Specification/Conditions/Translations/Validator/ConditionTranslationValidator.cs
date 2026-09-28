using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions.Translations;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Entities.Specification.Translations;

namespace ShagOxServer.Application.Services.Specification.Conditions.Translations.Validator;
public class ConditionTranslationValidator
    : BaseTranslationValidator<Condition,
        ConditionTranslation>
{
    public ConditionTranslationValidator(
        IRepository<ConditionTranslation> conditionRepository,
        IConditionTranslationExistsRepository conditionExistsRepository
    ) : base(conditionRepository, conditionExistsRepository)
    {
    }
}