using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Translations;
public class AttributeDictionaryValueTranslationExistsRepository
    : ExistsTranslationRepository<AttributeDefinitionTranslation>,
      IAttributeDictionaryValueTranslationExistsRepository
{
    public AttributeDictionaryValueTranslationExistsRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsByNameAsync(
        string name)
    {
        return await _db.CityTranslations
            .AnyAsync(x => x.Name == name);
    }
}