using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Query;
public partial class AttributeDefinitionQueryRepository
    : SearchRepository<AttributeDefinition, AttributeDefinitionSearchFilter>,
      IAttributeDefinitionQueryRepository
{
    public async Task<List<AttributeDefinitionCacheInfo>>
        GetCacheInfosByAttributeDictionaryAsync(
       long dictionaryId,
       CancellationToken cancellationToken = default)
    {
        return await _db.AttributeDefinitions
            .Where(x => x.DictionaryId == dictionaryId)
            .SelectCacheInfo()
            .ToListAsync(cancellationToken);
    }
}
