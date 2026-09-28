using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions.Translations;
using ShagOxServer.Domain.Entities.Advertisements.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Translations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Translations;
public class AttributeDefinitionTranslationExistsRepository
    : ExistsTranslationRepository<AttributeDefinition,
        AttributeDefinitionTranslation>,
      IAttributeDefinitionTranslationExistsRepository
{
    public AttributeDefinitionTranslationExistsRepository(AppDbContext db)
        : base(db)
    { }

    protected override IQueryable<AttributeDefinitionTranslation> ApplyIncludes(
        IQueryable<AttributeDefinitionTranslation> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<AttributeDefinitionTranslation> ApplyIdentificatorFilter(
        IQueryable<AttributeDefinitionTranslation> query,
        string identificator)
    {
        return query.Where(x => x.Translatable.Key == identificator);
    }
}