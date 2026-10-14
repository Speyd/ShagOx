using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Query;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Query;
public partial class AttributeDictionaryValueQueryRepository
    : SearchRepository<AttributeDictionaryValue, 
        AttributeDictionaryValueSearchFilter>,
      IAttributeDictionaryValueQueryRepository
{
    public AttributeDictionaryValueQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<AttributeDictionaryValue?> GetByIdentificatorAsync(
        string identificator,
        long? parentId = null,
        CancellationToken cancellationToken = default)
    {
        return await _db.AttributeDictionaryValues
            .WithIncludes()
            .FirstOrDefaultAsync(x =>
                x.Code == identificator &&
                (parentId.HasValue && x.DictionaryId == parentId),
                cancellationToken);
    }

    public async Task<List<AttributeDictionaryValue>> GetByIdsAsync(
        IEnumerable<long> ids,
        CancellationToken cancellationToken = default)
    {
        return await _db.AttributeDictionaryValues
            .WithIncludes()
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<AttributeDictionaryValue?> GetAsync(
        string dictionaryCode,
        long valueId,
        CancellationToken cancellationToken = default)
    {
        return await _db.AttributeDictionaryValues
            .WithIncludes()
            .FirstOrDefaultAsync(x =>
                x.Dictionary.Code == dictionaryCode &&
                x.Id == valueId,
                cancellationToken);
    }

    public async Task<AttributeDictionaryValue?> GetAsync(
        long dictionaryId,
        long valueId,
        CancellationToken cancellationToken = default)
    {
        return await _db.AttributeDictionaryValues
            .WithIncludes()
            .FirstOrDefaultAsync(x =>
                x.DictionaryId == dictionaryId &&
                x.Id == valueId,
                cancellationToken);
    }

    public async Task<PagedResult<AttributeDictionaryValue>> GetByDictionaryAsync(
        long dictionaryId, 
        PaginationParams pagination,
        CancellationToken cancellationToken = default)
    {
        return await _db.AttributeDictionaryValues
            .WithIncludes()
            .Where(x => x.DictionaryId == dictionaryId)
            .ToPagedResultAsync(pagination,
                cancellationToken);
    }
}
