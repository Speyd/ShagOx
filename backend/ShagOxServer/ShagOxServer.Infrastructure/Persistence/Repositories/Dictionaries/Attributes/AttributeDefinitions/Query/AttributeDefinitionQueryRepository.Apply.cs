using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Query;

public partial class AttributeDefinitionQueryRepository
    : QueryRepository<AttributeDefinition, AttributeDefinitionSearchFilter>,
      IAttributeDefinitionQueryRepository
{
    protected override IQueryable<AttributeDefinition> ApplyIncludes(
        IQueryable<AttributeDefinition> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<AttributeDefinition> ApplyFilter(
        IQueryable<AttributeDefinition> query,
        AttributeDefinitionSearchFilter filter)
    {
        return query.Filter(filter);
    }
}