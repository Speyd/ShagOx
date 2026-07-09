using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories;
public class CategoryExistsRepository : BaseRepository, ICategoryExistsRepository
{
    public CategoryExistsRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<bool> ExistsIdAsync(int id)
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

}