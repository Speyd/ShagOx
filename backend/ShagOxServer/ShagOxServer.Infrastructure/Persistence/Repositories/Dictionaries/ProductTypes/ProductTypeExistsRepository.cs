using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.ProductTypes;
public class ProductTypeExistsRepository
    : BaseRepository, IProductTypeExistsRepository
{
    public ProductTypeExistsRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await _db.ProductTypes
            .AnyAsync(c => c.Id == id);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _db.ProductTypes
            .AnyAsync(c => c.Name == name);
    }
}