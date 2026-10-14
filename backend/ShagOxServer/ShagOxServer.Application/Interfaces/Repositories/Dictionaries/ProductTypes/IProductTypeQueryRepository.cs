using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes;
public interface IProductTypeQueryRepository
    : ISearchTranslatableRepository<ProductType, 
        ProductTypeSearchFilter>
{
}
