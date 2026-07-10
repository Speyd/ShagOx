using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Conditions;
public class ConditionRepository : BaseRepository, IConditionRepository
{
    public ConditionRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<Condition?> GetByIdAsync(int id)
    {
        return await _db.Conditions
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public void AddAsync(Condition condition)
    {
        _db.Conditions.Add(condition);
    }

    public bool UpdateAsync(Condition condition)
    {
        _db.Conditions.Update(condition);
        return true;
    }

    public void DeleteAsync(Condition condition)
    {
        _db.Conditions.Remove(condition);
    }
}