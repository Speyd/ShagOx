using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries.Categories;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories;

public class CategoryRepository : BaseRepository, ICategoryRepository
{
    public CategoryRepository(AppDbContext db) 
        : base(db)
    {}

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _db.Categories
            .FirstOrDefaultAsync(c => c.Id == id);
    }

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
}
