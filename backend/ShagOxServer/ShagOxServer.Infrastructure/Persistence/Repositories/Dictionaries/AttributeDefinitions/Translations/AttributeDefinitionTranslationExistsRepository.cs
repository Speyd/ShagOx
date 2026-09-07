using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.AttributeDefinitions.Translations;
public class AttributeDefinitionTranslationExistsRepository
    : ExistsTranslationRepository<AttributeDefinitionTranslation>,
      IAttributeDefinitionTranslationExistsRepository
{
    public AttributeDefinitionTranslationExistsRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsByNameAsync(
        string name)
    {
        return await _db.CityTranslations
            .AnyAsync(x => x.Name == name);
    }
}