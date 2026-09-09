using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes.Translations;
public interface IProductTypeTranslationQueryRepository
    : IQueryTranslationRepository<ProductTypeTranslation>
{
    Task<PagedResult<ProductTypeTranslation>> Search(
       ProductTypeTranslationSearchFilter filter,
       PaginationParams pagination);
}