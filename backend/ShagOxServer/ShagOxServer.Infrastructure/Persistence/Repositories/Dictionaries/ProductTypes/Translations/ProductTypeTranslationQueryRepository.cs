using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.Categories.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.ProductTypes.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.ProductTypes.Translations.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.ProductTypes.Translations;
public class ProductTypeTranslationQueryRepository
    : QueryTranslationRepository<ProductTypeTranslation, 
        ProductTypeTranslationSearchFilter>,
      IProductTypeTranslationQueryRepository
{
    public ProductTypeTranslationQueryRepository(AppDbContext db)
        : base(db)
    { }


    protected override IQueryable<ProductTypeTranslation> ApplyFilter(
       IQueryable<ProductTypeTranslation> query,
       ProductTypeTranslationSearchFilter filter)
    {
        return query.Filter(filter);
    }
}