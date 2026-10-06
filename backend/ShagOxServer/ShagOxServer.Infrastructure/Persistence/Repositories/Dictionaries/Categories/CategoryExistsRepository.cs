using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories;
public class CategoryExistsRepository
    : ExistsRepository<Category>,
      ICategoryExistsRepository
{
    public CategoryExistsRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsAsync(
        string name,
        long productTypeId)
    {
        return await _db.Categories
            .AnyAsync(c =>
                c.Code == name &&
                c.ProductTypeId == productTypeId);
    }

    public async Task<bool> ExistsByCodeAsync(
        string code)
    {
        return await _db.Categories
            .AnyAsync(c => c.Code == code);
    }

    public async Task<bool> ExistsByProductTypeAsync(
        long productTypeId)
    {
        return await _db.Categories
            .AnyAsync(c => c.ProductTypeId == productTypeId);
    }
}