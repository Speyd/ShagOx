using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Translations;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
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
        AppDbContext db,
        IUserContext userContext
    )
        : base(db, userContext)
    {
    }


    protected override IQueryable<AttributeDictionaryValueTranslation> ApplyIncludes(
         IQueryable<AttributeDictionaryValueTranslation> query,
        EntityStatus? status = null)
    {
        return query.WithIncludes(status);
    }

    protected override IQueryable<AttributeDictionaryValueTranslation> ApplyIdentificatorFilter(
        IQueryable<AttributeDictionaryValueTranslation> query,
        string identificator)
    {
        return query.Where(x => x.Translatable.Code == identificator);
    }
}
