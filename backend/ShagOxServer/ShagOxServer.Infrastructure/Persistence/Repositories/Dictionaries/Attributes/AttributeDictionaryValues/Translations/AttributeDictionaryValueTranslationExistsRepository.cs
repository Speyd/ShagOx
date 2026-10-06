using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Translations;
public class AttributeDictionaryValueTranslationExistsRepository
    : ExistsTranslationRepository<AttributeDictionaryValue,
        AttributeDictionaryValueTranslation>,
      IAttributeDictionaryValueTranslationExistsRepository
{
    public AttributeDictionaryValueTranslationExistsRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    protected override IQueryable<AttributeDictionaryValueTranslation> ApplyIncludes(
         IQueryable<AttributeDictionaryValueTranslation> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<AttributeDictionaryValueTranslation> ApplyIdentificatorFilter(
        IQueryable<AttributeDictionaryValueTranslation> query,
        string identificator)
    {
        return query.Where(x => x.Translatable.Code == identificator);
    }
}