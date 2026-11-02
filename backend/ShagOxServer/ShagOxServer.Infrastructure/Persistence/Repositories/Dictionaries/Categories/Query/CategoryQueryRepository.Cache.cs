using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Query;

public partial class CategoryQueryRepository
    : SearchRepository<Category, CategorySearchFilter>,
      ICategoryQueryRepository
{
    public async Task<List<CategoryCacheInfo>> GetCacheInfosByProductTypeAsync(
        long productTypeId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Categories
            .WithStatusFilter(status)
            .Where(c => c.ProductTypeId == productTypeId)
            .SelectCacheInfo()
            .ToListAsync(cancellationToken);
    }
}
