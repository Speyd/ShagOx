using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries.Categories;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories;
public class CategoryQueryRepository : BaseRepository, ICategoryQueryRepository
{
    public CategoryQueryRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _db.Categories.WithIncludes()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Category?> GetByNameAsync(string name)
    {
        return await _db.Categories.WithIncludes()
            .FirstOrDefaultAsync(c => c.Name == name);
    }

    public async Task<List<Category>> GetByProductTypeAsync(ProductType type)
    {
        return await _db.Categories.WithIncludes()
            .Where(c => c.ProductType == type)
            .ToListAsync();
    }
}
