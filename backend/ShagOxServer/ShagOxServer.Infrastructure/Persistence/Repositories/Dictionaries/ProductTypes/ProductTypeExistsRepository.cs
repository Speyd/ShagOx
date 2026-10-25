using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.ProductTypes;
public class ProductTypeExistsRepository
    : ExistsRepository<ProductType>,
      IProductTypeExistsRepository
{
    public ProductTypeExistsRepository(
        AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsByCodeAsync(
        string code,
        CancellationToken cancellationToken = default)
    {
        return await _db.ProductTypes
            .AnyAsync(c => c.Code == code,
                cancellationToken);
    }
}
