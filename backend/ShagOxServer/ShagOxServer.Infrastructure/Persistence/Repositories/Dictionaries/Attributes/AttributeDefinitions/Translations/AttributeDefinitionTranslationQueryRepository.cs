using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Translations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Translations;
public class AttributeDefinitionTranslationQueryRepository
    : SearchTranslationRepository<AttributeDefinition,
        AttributeDefinitionTranslation,
        AttributeDefinitionTranslationSearchFilter>,
      IAttributeDefinitionTranslationQueryRepository
{
    public AttributeDefinitionTranslationQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    protected override IQueryable<AttributeDefinitionTranslation> ApplyIncludes(
         IQueryable<AttributeDefinitionTranslation> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<AttributeDefinitionTranslation> ApplyFilter(
       IQueryable<AttributeDefinitionTranslation> query,
       AttributeDefinitionTranslationSearchFilter filter)
    {
        return query.Filter(filter);
    }

    protected override IQueryable<AttributeDefinitionTranslation> ApplyIdentificatorFilter(
        IQueryable<AttributeDefinitionTranslation> query,
        string identificator)
    {
        return query.Where(x => x.Translatable.Key == identificator);
    }
}
