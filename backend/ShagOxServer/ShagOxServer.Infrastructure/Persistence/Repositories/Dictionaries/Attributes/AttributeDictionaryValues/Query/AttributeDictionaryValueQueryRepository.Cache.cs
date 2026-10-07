using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.DTOs.Dictionaries.Attributes.AttributeDictionaryValues.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Query;

public partial class AttributeDictionaryValueQueryRepository
    : SearchRepository<AttributeDictionaryValue,
        AttributeDictionaryValueSearchFilter>,
      IAttributeDictionaryValueQueryRepository
{
    public async Task<List<AttributeDictionaryValueCacheInfo>> GetCacheInfosByDictionaryAsync(
        long dictionaryId)
    {
        return await _db.AttributeDictionaryValues
            .Where(x => x.DictionaryId == dictionaryId)
            .SelectCacheInfo()
            .ToListAsync();
    }
}
