using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes.Translations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes.Translations;
public interface IProductTypeTranslationQueryRepository
    : ITranslationQueryRepository<ProductType,
        ProductTypeTranslation, 
        ProductTypeTranslationSearchFilter>
{
}