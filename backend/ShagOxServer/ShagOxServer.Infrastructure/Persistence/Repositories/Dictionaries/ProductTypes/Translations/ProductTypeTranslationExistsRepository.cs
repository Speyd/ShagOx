using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.ProductTypes.Translations;
public class ProductTypeTranslationExistsRepository
    : ExistsTranslationRepository<ProductTypeTranslation>,
      IProductTypeTranslationExistsRepository
{
    public ProductTypeTranslationExistsRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsByNameAsync(
        string name)
    {
        return await _db.ProductTypeTranslations
            .AnyAsync(x => x.Name == name);
    }
}