using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Query;
public partial class AttributeDefinitionQueryRepository 
    : QueryRepository<AttributeDefinition, AttributeDefinitionSearchFilter>, 
      IAttributeDefinitionQueryRepository
{
    public AttributeDefinitionQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<List<AttributeDefinition>> GetByIdsAsync(
        List<long> ids)
    {
        return await _db.AttributeDefinitions
            .WithIncludes()
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }

    public async Task<List<long>> GetIdsByAttributeDictionaryAsync(
        long dictionaryId)
    {
        return await _db.AttributeDefinitions
            .Where(x => x.DictionaryId == dictionaryId)
            .Select(x => x.Id)
            .ToListAsync();
    }

    public async Task<List<AttributeDefinition>> GetByKeysAsync(
        long categoryId,
        IEnumerable<string> keys)
    {
        return await _db.AttributeDefinitions
            .WithIncludes()
            .Where(x =>
                x.CategoryId == categoryId &&
                keys.Contains(x.Key))
            .ToListAsync();
    }

    public async Task<AttributeDefinition?> GetByIdentificatorAsync(
        string identificator,
        long? parentId = null)
    {
        return await _db.AttributeDefinitions
            .WithIncludes()
            .FirstOrDefaultAsync(x =>
                x.Key == identificator &&
                (parentId.HasValue && x.CategoryId == parentId));
    }
}