using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.ProductTypes.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.ProductTypes;
public class ProductTypeQueryRepository
    : BaseRepository, IProductTypeQueryRepository
{
    public ProductTypeQueryRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<ProductType?> GetByIdAsync(int id)
    {
        return await _db.ProductTypes
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<PagedResult<ProductType>> GetPagedAsync(
        PaginationParams pagination)
    {
        return await _db.ProductTypes
            .ToPagedResultAsync(pagination);
    }

    public async Task<PagedResult<ProductType>> Search(
        ProductTypeSearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.ProductTypes
            .Filter(filter)
            .ToPagedResultAsync(pagination);
    }
}