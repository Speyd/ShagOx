using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaries.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaries.Query;
public partial class AttributeDictionaryQueryRepository
    : SearchRepository<AttributeDictionary, AttributeDictionarySearchFilter>,
      IAttributeDictionaryQueryRepository
{
    protected override IQueryable<AttributeDictionary> ApplyIncludes(
        IQueryable<AttributeDictionary> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<AttributeDictionary> ApplyFilter(
        IQueryable<AttributeDictionary> query,
        AttributeDictionarySearchFilter filter)
    {
        return query.Filter(filter);
    }
}
