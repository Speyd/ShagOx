using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Infrastructure.Interfaces.Specification;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification;
public class ConditionRepository : BaseRepository, IConditionRepository
{
    public ConditionRepository(AppDbContext db)
        : base(db)
    { }

    public async Task AddAsync(Condition condition)
    {
        await _db.Conditions.AddAsync(condition);

        await _db.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(Condition condition)
    {
        _db.Conditions.Update(condition);

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task DeleteAsync(Condition condition)
    {
        _db.Conditions.Remove(condition);

        await _db.SaveChangesAsync();
    }


    public async Task<Condition?> GetByIdAsync(int id)
    {
        return await _db.Conditions
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Condition?> GetByNameAsync(string name)
    {
        return await _db.Conditions
            .FirstOrDefaultAsync(x => x.Name == name);
    }


    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _db.Conditions
            .AnyAsync(x => x.Name == name);
    }
}
