using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaries.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaries.Query;
public partial class AttributeDictionaryQueryRepository
    : QueryRepository<AttributeDictionary, AttributeDictionarySearchFilter>,
      IAttributeDictionaryQueryRepository
{
    public AttributeDictionaryQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<PagedResult<AttributeDefinition>> GetByDictionaryAsync(
        long attributeDictionaryId,
        PaginationParams pagination)
    {
        return await _db.AttributeDefinitions
            .WithIncludes()
            .Where(x => x.DictionaryId == attributeDictionaryId)
            .ToPagedResultAsync(pagination);
    }
}