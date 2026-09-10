using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.Categories.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Translations;
public interface ICategoryTranslationQueryRepository
    : IQueryTranslationRepository<CategoryTranslation>
{
    Task<PagedResult<CategoryTranslation>> Search(
       CategoryTranslationSearchFilter filter,
       PaginationParams pagination);
}