using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Query;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Location.Cities.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Query;
public partial class CategoryQueryRepository 
    : SearchRepository<Category, CategorySearchFilter>, 
      ICategoryQueryRepository
{
    public CategoryQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


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
