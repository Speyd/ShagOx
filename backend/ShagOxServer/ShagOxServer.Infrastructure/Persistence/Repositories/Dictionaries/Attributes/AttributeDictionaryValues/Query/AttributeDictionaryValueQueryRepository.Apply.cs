using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Query;

public partial class AttributeDictionaryValueQueryRepository
    : QueryRepository<AttributeDictionaryValue,
        AttributeDictionaryValueSearchFilter>,
      IAttributeDictionaryValueQueryRepository
{
    protected override IQueryable<AttributeDictionaryValue> ApplyIncludes(
        IQueryable<AttributeDictionaryValue> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<AttributeDictionaryValue> ApplyFilter(
        IQueryable<AttributeDictionaryValue> query,
        AttributeDictionaryValueSearchFilter filter)
    {
        return query.Filter(filter);
    }
}