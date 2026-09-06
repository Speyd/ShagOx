using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Base;
using ShagOxServer.Infrastructure.Persistence.DbContexts;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base;
public class ExistsRepository<T>
    : RepositoryContext, IExistsRepository<T>
    where T : BaseEntity
{
    public ExistsRepository(AppDbContext db)
        : base(db)
    {
    }


    public virtual async Task<bool> ExistsByIdAsync(int id)
    {
        return await _db.Set<T>()
            .AnyAsync(x => EF.Property<int>(x, "Id") == id);
    }
}