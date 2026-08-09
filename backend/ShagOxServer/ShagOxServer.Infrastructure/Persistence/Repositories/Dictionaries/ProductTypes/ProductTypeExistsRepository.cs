using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.ProductTypes;
public class ProductTypeExistsRepository
    : ExistsRepository<ProductType>,
      IProductTypeExistsRepository
{
    public ProductTypeExistsRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsByNameAsync(
        string name)
    {
        return await _db.ProductTypes
            .AnyAsync(c => c.Name == name);
    }
}