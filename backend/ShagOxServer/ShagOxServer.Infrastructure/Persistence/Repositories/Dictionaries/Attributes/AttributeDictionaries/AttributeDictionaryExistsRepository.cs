using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDictionaries;
public class AttributeDictionaryExistsRepository
    : ExistsRepository<AttributeDictionary>,
      IAttributeDictionaryExistsRepository
{
    public AttributeDictionaryExistsRepository(
        AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsByCodeAsync(
        string code)
    {
        return await _db.AttributeDictionaries
            .AnyAsync(x => x.Code == code);
    }
}