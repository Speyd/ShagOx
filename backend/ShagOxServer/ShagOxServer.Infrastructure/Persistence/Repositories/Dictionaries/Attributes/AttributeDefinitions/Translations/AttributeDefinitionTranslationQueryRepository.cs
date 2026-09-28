using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Translations.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Translations;
public class AttributeDefinitionTranslationQueryRepository
    : QueryTranslationRepository<AttributeDefinitionTranslation,
        AttributeDefinitionTranslationSearchFilter>,
      IAttributeDefinitionTranslationQueryRepository
{
    public AttributeDefinitionTranslationQueryRepository(AppDbContext db)
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

    public override async Task<AttributeDefinitionTranslation?> GetByIdentificatorAsync(
        string identificator,
        string language)
    {
        return await _db.AttributeDefinitionTranslations
          .FirstOrDefaultAsync(x =>
            x.Translatable.Key == identificator &&
            x.Language == language);
    }
}