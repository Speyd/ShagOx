using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.AttributeDefinitions.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.AttributeDefinitions.Translations;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.AttributeDefinitions.Translations.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.AttributeDefinitions.Translations;
public class AttributeDefinitionTranslationQueryRepository
    : QueryTranslationRepository<AttributeDefinitionTranslation>,
      IAttributeDefinitionTranslationQueryRepository
{
    public AttributeDefinitionTranslationQueryRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<PagedResult<AttributeDefinitionTranslation>> Search(
        AttributeDefinitionTranslationSearchFilter filter,
        PaginationParams pagination)
    {
        return await _db.AttributeDefinitionTranslations
            .Filter(filter)
            .ToPagedResultAsync(pagination);
    }
}