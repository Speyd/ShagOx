using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Translations;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Translations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Translations;
public class CategoryTranslationExistsRepository
    : ExistsTranslationRepository<Category,
        CategoryTranslation>,
      ICategoryTranslationExistsRepository
{
    public CategoryTranslationExistsRepository(
        AppDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }


    protected override IQueryable<CategoryTranslation> ApplyIncludes(
        IQueryable<CategoryTranslation> query,
        EntityStatus? status = null)
    {
        return query.WithIncludes(status);
    }

    protected override IQueryable<CategoryTranslation> ApplyIdentificatorFilter(
        IQueryable<CategoryTranslation> query,
        string identificator)
    {
        return query.Where(x => x.Translatable.Code == identificator);
    }
}
