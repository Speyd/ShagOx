using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories;
public class CategoryQueryRepository 
    : QueryRepository<Category, CategorySearchFilter>, 
      ICategoryQueryRepository
{
    public CategoryQueryRepository(AppDbContext db)
        : base(db)
    { }


    protected override IQueryable<Category> ApplyIncludes(
        IQueryable<Category> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<Category> ApplyFilter(
        IQueryable<Category> query,
        CategorySearchFilter filter)
    {
        return query.Filter(filter);
    }

    public async Task<Category?> GetByIdentificatorAsync(
        string identificator,
        long? parentId = null)
    {
        return await _db.Categories
            .WithIncludes()
            .FirstOrDefaultAsync(x =>
                x.Code == identificator &&
                (parentId.HasValue && x.ProductTypeId == parentId));
    }

    public async Task<PagedResult<Category>> GetByProductTypeAsync(
        long productTypeId,
        PaginationParams pagination)
    {
        return await _db.Categories
            .WithIncludes()
            .Where(c => c.ProductTypeId == productTypeId)
            .ToPagedResultAsync(pagination);
    }
}