using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Base;
public class ExistsRepository<T>
    : RepositoryContext, IExistsRepository<T>
    where T : class
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