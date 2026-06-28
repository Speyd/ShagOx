using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries;

public class CategoryRepository : BaseRepository, ICategoryRepository
{
    public CategoryRepository(AppDbContext db) 
        : base(db)
    {}

    public async Task AddAsync(Category category)
    {
        await _db.Categories.AddAsync(category);

        await _db.SaveChangesAsync();
    }

    public async Task<bool> ExistsNameAsync(string name)
    {
        return await _db.Categories.AnyAsync(c => c.Name == name);
    }

    public async Task<bool> ExistsProductTypeAsync(ProductType type)
    {
        return await _db.Categories.AnyAsync(c => c.ProductType == type);
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _db.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Category?> GetByNameAsync(string name)
    {
        return await _db.Categories.FirstOrDefaultAsync(c => c.Name == name);
    }

    public async Task<Category?> GetByProductTypeAsync(ProductType type)
    {
        return await _db.Categories.FirstOrDefaultAsync(c => c.ProductType == type);
    }

    public async Task<bool> UpdateAsync(Category category)
    {
        _db.Categories.Update(category);

        await _db.SaveChangesAsync();
        return true;
    }
}
