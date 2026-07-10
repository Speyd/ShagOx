using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Domain.Entities.Dictionaries;

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

    public void Add(Category category)
    {
        _db.Categories.AddAsync(category);
    }

    public bool Update(Category category)
    {
        _db.Categories.Update(category);
        return true;
    }

    public void Delete(Category category)
    {
        _db.Categories.Remove(category);
    }
}