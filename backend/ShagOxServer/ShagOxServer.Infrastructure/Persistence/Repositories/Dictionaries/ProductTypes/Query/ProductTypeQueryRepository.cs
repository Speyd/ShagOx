using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.ProductTypes.Query;
public partial class ProductTypeQueryRepository
    : QueryRepository<ProductType, ProductTypeSearchFilter>, 
      IProductTypeQueryRepository
{
    public ProductTypeQueryRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<ProductType?> GetByIdentificatorAsync(
        string identificator,
        long? parentId = null)
    {
        return await _db.ProductTypes
            .FirstOrDefaultAsync(x =>
                x.Code == identificator);
    }
}