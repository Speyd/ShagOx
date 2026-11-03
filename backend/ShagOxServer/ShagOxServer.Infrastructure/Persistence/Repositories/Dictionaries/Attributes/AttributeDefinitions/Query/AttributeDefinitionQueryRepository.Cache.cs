using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDefinitions.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
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
        CancellationToken cancellationToken = default,
        EntityStatus status = EntityStatus.Active)
    {
        return await _db.AttributeDefinitions
            .WithStatusFilter(status)
            .Where(x => x.DictionaryId == dictionaryId)
            .SelectCacheInfo(status)
            .ToListAsync(cancellationToken);
    }
}
