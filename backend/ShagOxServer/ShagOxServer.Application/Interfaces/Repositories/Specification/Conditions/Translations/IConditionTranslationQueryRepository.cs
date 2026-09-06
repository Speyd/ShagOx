using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Specification.Translations;
using ShagOxServer.Domain.Filters.Specification.Conditions.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions.Translations;
public interface IConditionTranslationQueryRepository
    : IQueryTranslationRepository<ConditionTranslation>
{
    Task<PagedResult<ConditionTranslation>> Search(
       ConditionTranslationSearchFilter filter,
       PaginationParams pagination);
}