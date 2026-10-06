using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Translations;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Translations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Translations;
public class CategoryTranslationExistsRepository
    : ExistsTranslationRepository<Category,
        CategoryTranslation>,
      ICategoryTranslationExistsRepository
{
    public CategoryTranslationExistsRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    protected override IQueryable<CategoryTranslation> ApplyIncludes(
        IQueryable<CategoryTranslation> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<CategoryTranslation> ApplyIdentificatorFilter(
        IQueryable<CategoryTranslation> query,
        string identificator)
    {
        return query.Where(x => x.Translatable.Code == identificator);
    }
}