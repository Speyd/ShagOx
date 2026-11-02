using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Attributes.AttributeDictionaries;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Exists;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;

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
        string code,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.AttributeDictionaries
            .WithStatusFilter(status)
            .AnyAsync(x => x.Code == code,
                cancellationToken);
    }
}
