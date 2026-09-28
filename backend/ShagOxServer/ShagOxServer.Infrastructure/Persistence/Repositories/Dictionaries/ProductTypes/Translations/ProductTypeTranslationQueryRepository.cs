using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Categories.Translations.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.ProductTypes.Translations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.ProductTypes.Translations;
public class ProductTypeTranslationQueryRepository
    : QueryTranslationRepository<ProductTypeTranslation, 
        ProductTypeTranslationSearchFilter>,
      IProductTypeTranslationQueryRepository
{
    public ProductTypeTranslationQueryRepository(AppDbContext db)
        : base(db)
    { }


    protected override IQueryable<ProductTypeTranslation> ApplyIncludes(
         IQueryable<ProductTypeTranslation> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<ProductTypeTranslation> ApplyFilter(
       IQueryable<ProductTypeTranslation> query,
       ProductTypeTranslationSearchFilter filter)
    {
        return query.Filter(filter);
    }

    public override async Task<ProductTypeTranslation?> GetByIdentificatorAsync(
        string identificator,
        string language)
    {
        return await _db.ProductTypeTranslations
          .FirstOrDefaultAsync(x =>
            x.Translatable.Code == identificator &&
            x.Language == language);
    }
}