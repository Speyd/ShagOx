using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Translations;
public class AttributeDictionaryValueTranslationExistsRepository
    : ExistsTranslationRepository<AttributeDictionaryValue,
        AttributeDictionaryValueTranslation>,
      IAttributeDictionaryValueTranslationExistsRepository
{
    public AttributeDictionaryValueTranslationExistsRepository(AppDbContext db)
        : base(db)
    { }


    protected override IQueryable<AttributeDictionaryValueTranslation> ApplyIncludes(
         IQueryable<AttributeDictionaryValueTranslation> query)
    {
        return query.WithIncludes();
    }
}