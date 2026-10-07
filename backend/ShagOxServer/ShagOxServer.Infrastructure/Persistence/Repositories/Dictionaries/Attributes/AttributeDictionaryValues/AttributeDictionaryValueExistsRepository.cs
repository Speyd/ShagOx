using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaryValues;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaryValues;
public class AttributeDictionaryValueExistsRepository
    : ExistsRepository<AttributeDictionaryValue>,
      IAttributeDictionaryValueExistsRepository
{
    public AttributeDictionaryValueExistsRepository(
        AppDbContext db)
        : base(db)
    { }

    public async Task<bool> ExistsAsync(
        long dictionaryId,
        string code)
    {
        return await _db.AttributeDictionaryValues
            .AnyAsync(x => 
                x.DictionaryId == dictionaryId && 
                x.Code == code);
    }

    public async Task<bool> ExistsByCodeAsync(
        string code)
    {
        return await _db.AttributeDictionaryValues
            .AnyAsync(x => x.Code == code);
    }
}