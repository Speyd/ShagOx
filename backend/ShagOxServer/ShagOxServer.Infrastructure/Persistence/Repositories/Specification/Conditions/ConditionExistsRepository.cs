using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Conditions;
public class ConditionExistsRepository : BaseRepository, IConditionExistsRepository
{
    public ConditionExistsRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await _db.Conditions
            .AnyAsync(x => x.Id == id);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _db.Conditions
            .AnyAsync(x => x.Name == name);
    }
}
