using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Query;
public partial class AttributeDefinitionQueryRepository 
    : SearchRepository<AttributeDefinition, AttributeDefinitionSearchFilter>, 
      IAttributeDefinitionQueryRepository
{
    public AttributeDefinitionQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<List<AttributeDefinition>> GetByIdsAsync(
        List<long> ids,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.AttributeDefinitions
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<List<long>> GetIdsByAttributeDictionaryAsync(
        long dictionaryId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.AttributeDefinitions
            .WithStatusFilter(status)
            .Where(x => x.DictionaryId == dictionaryId)
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<AttributeDefinition>> GetByKeysAsync(
        long categoryId,
        IEnumerable<string> keys,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.AttributeDefinitions
            .WithIncludes()
            .WithStatusFilter(status)
            .Where(x =>
                x.CategoryId == categoryId &&
                keys.Contains(x.Key))
            .ToListAsync(cancellationToken);
    }

    public async Task<AttributeDefinition?> GetByIdentificatorAsync(
        string identificator,
        long? parentId = null,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.AttributeDefinitions
            .WithIncludes()
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(x =>
                x.Key == identificator &&
                (parentId.HasValue && x.CategoryId == parentId),
                cancellationToken);
    }

    public async Task<List<AttributeDefinition>> GetByCategoryAsync(
        long categoryId, 
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.AttributeDefinitions
           .WithIncludes()
           .WithStatusFilter(status)
           .Where(x => x.CategoryId == categoryId)
           .ToListAsync(cancellationToken);
    }
}
