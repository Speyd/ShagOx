using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes.Translations;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.ProductTypes.Translations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.ProductTypes.Translations;
public class ProductTypeTranslationExistsRepository
    : ExistsTranslationRepository<ProductType, 
        ProductTypeTranslation>,
      IProductTypeTranslationExistsRepository
{
    public ProductTypeTranslationExistsRepository(AppDbContext db)
        : base(db)
    { }


    protected override IQueryable<ProductTypeTranslation> ApplyIncludes(
         IQueryable<ProductTypeTranslation> query)
    {
        return query.WithIncludes();
    }
}