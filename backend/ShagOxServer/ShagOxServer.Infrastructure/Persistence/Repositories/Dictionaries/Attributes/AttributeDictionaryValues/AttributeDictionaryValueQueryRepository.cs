using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Domain.Filters.Dictionaries.Attributes.AttributeDefinitionValues;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues;
public class AttributeDictionaryValueQueryRepository
    : QueryRepository<AttributeDictionaryValue, 
        AttributeDictionaryValueSearchFilter>,
      IAttributeDictionaryValueQueryRepository
{
    public AttributeDictionaryValueQueryRepository(AppDbContext db)
        : base(db)
    { }

    protected override IQueryable<AttributeDictionaryValue> ApplyFilter(
        IQueryable<AttributeDictionaryValue> query,
        AttributeDictionaryValueSearchFilter filter)
    {
        return query.Filter(filter);
    }

    public async Task<PagedResult<AttributeDictionaryValue>> GetByDictionaryAsync(
        long dictionaryId, 
        PaginationParams pagination)
    {
        return await _db.AttributeDictionaryValues
           .Where(x => x.DictionaryId == dictionaryId)
           .ToPagedResultAsync(pagination);
    }
}