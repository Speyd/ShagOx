using ShagOxServer.Application.DTOs.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Filters.Specification.Conditions;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Query;
public interface IConditionQueryService
    : ITranslatableQueryService<ConditionDto, 
        Condition, 
        ConditionSearchFilter>
{
}