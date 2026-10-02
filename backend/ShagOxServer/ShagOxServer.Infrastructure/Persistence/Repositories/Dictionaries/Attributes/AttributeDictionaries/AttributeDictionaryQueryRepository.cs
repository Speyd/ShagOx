using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitions;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaries.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaries;
public class AttributeDictionaryQueryRepository
    : QueryRepository<AttributeDictionary, AttributeDictionarySearchFilter>,
      IAttributeDictionaryQueryRepository
{
    public AttributeDictionaryQueryRepository(AppDbContext db)
        : base(db)
    { }

    protected override IQueryable<AttributeDictionary> ApplyIncludes(
        IQueryable<AttributeDictionary> query)
    {
        return query.WithIncludes();
    }

    protected override IQueryable<AttributeDictionary> ApplyFilter(
        IQueryable<AttributeDictionary> query,
        AttributeDictionarySearchFilter filter)
    {
        return query.Filter(filter);
    }

    public async Task<PagedResult<AttributeDefinition>> GetDefinitionsAsync(
        long attributeDictionaryId,
        PaginationParams pagination)
    {
        return await _db.AttributeDefinitions
            .WithIncludes()
            .Where(x => x.DictionaryId == attributeDictionaryId)
            .ToPagedResultAsync(pagination);
    }
}