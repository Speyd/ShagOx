using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.ProductTypes.Translations.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.ProductTypes.Translations;
public class ProductTypeTranslationQueryRepository
    : QueryTranslationRepository<ProductTypeTranslation>,
      IProductTypeTranslationQueryRepository
{
    public ProductTypeTranslationQueryRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<PagedResult<ProductTypeTranslation>> Search(
        ProductTypeTranslationSearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.ProductTypeTranslations
            .Filter(filter)
            .ToPagedResultAsync(pagination);
    }
}