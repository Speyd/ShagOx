using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Translations;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.Categories.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Translations.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Translations;
public class CategoryTranslationQueryRepository
    : QueryTranslationRepository<CategoryTranslation>,
      ICategoryTranslationQueryRepository
{
    public CategoryTranslationQueryRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<PagedResult<CategoryTranslation>> Search(
        CategoryTranslationSearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.CategoryTranslations
            .Filter(filter)
            .ToPagedResultAsync(pagination);
    }
}