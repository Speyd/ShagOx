using ShagOxServer.Application.DTOs.Specification.Conditions.Translations;
using ShagOxServer.Application.Interfaces.Services.Base.Translations;
using ShagOxServer.Domain.Filters.Specification.Conditions.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Query;
public interface IConditionTranslationQueryService
    : IQueryTranslationService<ConditionTranslationDto>
{
    Task<Result<PagedResult<ConditionTranslationDto>>> Search(
       ConditionTranslationSearchFilter filter,
       PaginationParams pagination);
}