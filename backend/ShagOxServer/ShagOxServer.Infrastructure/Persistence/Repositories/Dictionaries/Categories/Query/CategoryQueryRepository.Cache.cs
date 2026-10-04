using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Query;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Query;

public partial class CategoryQueryRepository
    : QueryRepository<Category, CategorySearchFilter>,
      ICategoryQueryRepository
{
    public async Task<List<CategoryCacheInfo>> GetCacheInfosByProductTypeAsync(
        long productTypeId)
    {
        return await _db.Categories
            .Where(c => c.ProductTypeId == productTypeId)
            .SelectCacheInfo()
            .ToListAsync();
    }
}