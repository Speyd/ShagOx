using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.ProductTypes.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.ProductTypes.Query;
public partial class ProductTypeQueryRepository
    : SearchRepository<ProductType, ProductTypeSearchFilter>,
      IProductTypeQueryRepository
{
    protected override IQueryable<ProductType> ApplyFilter(
        IQueryable<ProductType> query,
        ProductTypeSearchFilter filter)
    {
        return query.Filter(filter);
    }
}
