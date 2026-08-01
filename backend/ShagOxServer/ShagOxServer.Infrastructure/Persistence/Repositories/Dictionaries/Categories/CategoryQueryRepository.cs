using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.AttributeDefinitions.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories;
public class CategoryQueryRepository 
    : BaseRepository, ICategoryQueryRepository
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

    public async Task<PagedResult<Category>> GetPagedAsync(
        PaginationParams pagination)
    {
        return await _db.Categories
            .WithIncludes()
            .ToPagedResultAsync(pagination);
    }

    public async Task<PagedResult<Category>> GetByProductTypeAsync(
        int productTypeId,
        PaginationParams pagination)
    {
        return await _db.Categories
            .WithIncludes()
            .Where(c => c.ProductTypeId == productTypeId)
            .ToPagedResultAsync(pagination);
    }

    public async Task<PagedResult<Category>> Search(
        CategorySearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.Categories
            .WithIncludes()
            .Filter(filter)
            .ToPagedResultAsync(pagination);
    }
}