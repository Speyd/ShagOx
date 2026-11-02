using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Query;
public partial class AttributeDictionaryValueQueryRepository
    : SearchRepository<AttributeDictionaryValue,
        AttributeDictionaryValueSearchFilter>,
      IAttributeDictionaryValueQueryRepository
{
    public async Task<List<AttributeDictionaryValueCacheInfo>> GetCacheInfosByDictionaryAsync(
        long dictionaryId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.AttributeDictionaryValues
            .WithStatusFilter(status)
            .Where(x => x.DictionaryId == dictionaryId)
            .SelectCacheInfo()
            .ToListAsync();
    }
}
