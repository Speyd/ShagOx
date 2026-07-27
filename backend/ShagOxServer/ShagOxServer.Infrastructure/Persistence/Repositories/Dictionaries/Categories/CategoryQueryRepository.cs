using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.AttributeDefinitions.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories;
public class CategoryQueryRepository : BaseRepository, ICategoryQueryRepository
{
    public CategoryQueryRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _db.Categories
            .WithIncludes()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Category>> GetPagedAsync(
        PaginationParams pagination)
    {
        return await _db.Categories
            .WithIncludes()
            .WithPagination(pagination)
            .ToListAsync();
    }

    public async Task<Category?> GetByNameAsync(string name)
    {
        return await _db.Categories
            .WithIncludes()
            .FirstOrDefaultAsync(c => c.Name == name);
    }

    public async Task<List<Category>> GetByProductTypeAsync(
        ProductType type,
        PaginationParams pagination)
    {
        return await _db.Categories
            .WithIncludes()
            .Where(c => c.ProductType == type)
            .WithPagination(pagination)
            .ToListAsync();
    }

    public async Task<List<Category>> Search(
        CategorySearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.Categories
            .WithIncludes()
            .Filter(filter)
            .WithPagination(pagination)
            .ToListAsync();
    }
}