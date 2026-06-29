using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries;
using System.Xml.Linq;

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

    public async Task<bool> UpdateAsync(Category category)
    {
        _db.Categories.Update(category);

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task DeleteAsync(Category category)
    {
        _db.Categories.Remove(category);

        await _db.SaveChangesAsync();
    }

    private IQueryable<Category> Query()
    {
        return _db.Categories
            .Include(x => x.Attributes)
            .Include(x => x.Advertisements);        
    }


    public async  Task<bool> ExistsIdAsync(int id)
    {
        return await _db.Categories.AnyAsync(c => c.Id == id);
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
        return await Query()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Category?> GetByNameAsync(string name)
    {
        return await Query()
            .FirstOrDefaultAsync(c => c.Name == name);
    }

    public async Task<List<Category>> GetByProductTypeAsync(ProductType type)
    {
        return await Query()
            .Where(c => c.ProductType == type)
            .ToListAsync();
    }
}
