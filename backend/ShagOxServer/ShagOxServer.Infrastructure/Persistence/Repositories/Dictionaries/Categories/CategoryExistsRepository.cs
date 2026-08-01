using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories;
public class CategoryExistsRepository : BaseRepository, ICategoryExistsRepository
{
    public CategoryExistsRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsAsync(
        string name,
        int productTypeId)
    {
        return await _db.Categories
            .AnyAsync(c =>
                c.Name == name && c.ProductTypeId == productTypeId);
    }

    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await _db.Categories
            .AnyAsync(c => c.Id == id);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _db.Categories
            .AnyAsync(c => c.Name == name);
    }

    public async Task<bool> ExistsByProductTypeAsync(int productTypeId)
    {
        return await _db.Categories
            .AnyAsync(c => c.ProductTypeId == productTypeId);
    }
}