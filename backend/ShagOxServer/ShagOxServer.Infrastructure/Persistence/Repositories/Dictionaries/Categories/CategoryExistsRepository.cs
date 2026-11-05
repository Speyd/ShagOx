using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories;
public class CategoryExistsRepository
    : ExistsRepository<Category>,
      ICategoryExistsRepository
{
    public CategoryExistsRepository(
        AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsAsync(
        string name,
        long productTypeId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Categories
            .WithStatusFilter(status)
            .AnyAsync(c =>
                c.Code == name &&
                c.ProductTypeId == productTypeId,
                cancellationToken);
    }

    public async Task<bool> ExistsByCodeAsync(
        string code,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Categories
            .WithStatusFilter(status)
            .AnyAsync(c => c.Code == code,
                cancellationToken);
    }

    public async Task<bool> ExistsByProductTypeAsync(
        long productTypeId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Categories
            .WithStatusFilter(status)
            .AnyAsync(c => c.ProductTypeId == productTypeId,
                cancellationToken);
    }
}
