using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Translations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Translations;
public class AttributeDictionaryValueTranslationQueryRepository
    : QueryTranslationRepository<AttributeDictionaryValue,
        AttributeDictionaryValueTranslation,
        AttributeDictionaryValueTranslationSearchFilter>,
      IAttributeDictionaryValueTranslationQueryRepository
{
    public AttributeDictionaryValueTranslationQueryRepository(AppDbContext db)
        : base(db)
    { }


    protected override IQueryable<AttributeDictionaryValueTranslation> ApplyIncludes(
         IQueryable<AttributeDictionaryValueTranslation> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<AttributeDictionaryValueTranslation> ApplyFilter(
       IQueryable<AttributeDictionaryValueTranslation> query,
       AttributeDictionaryValueTranslationSearchFilter filter)
    {
        return query.Filter(filter);
    }
}