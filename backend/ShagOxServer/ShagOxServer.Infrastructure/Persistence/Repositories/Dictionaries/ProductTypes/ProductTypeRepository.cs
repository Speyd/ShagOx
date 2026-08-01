using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.ProductTypes;
internal class ProductTypeRepository 
    : BaseRepository, IProductTypeRepository
{
    public ProductTypeRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<ProductType?> GetByIdAsync(int id)
    {
        return await _db.ProductTypes
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public void Add(ProductType productType)
    {
        _db.ProductTypes.Add(productType);
    }

    public bool Update(ProductType productType)
    {
        _db.ProductTypes.Update(productType);
        return true;
    }

    public void Delete(ProductType productType)
    {
        _db.ProductTypes.Remove(productType);
    }
}