using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Translations;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.Categories.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Translations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Translations;
public class CategoryTranslationQueryRepository
    : QueryTranslationRepository<Category,
        CategoryTranslation, 
        CategoryTranslationSearchFilter>,
      ICategoryTranslationQueryRepository
{
    public CategoryTranslationQueryRepository(AppDbContext db)
        : base(db)
    { }


    protected override IQueryable<CategoryTranslation> ApplyIncludes(
         IQueryable<CategoryTranslation> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<CategoryTranslation> ApplyFilter(
       IQueryable<CategoryTranslation> query,
       CategoryTranslationSearchFilter filter)
    {
        return query.Filter(filter);
    }
}