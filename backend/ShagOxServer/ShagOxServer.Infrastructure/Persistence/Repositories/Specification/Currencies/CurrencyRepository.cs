using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Currencies;
public class CurrencyRepository : BaseRepository, ICurrencyRepository
{
    public CurrencyRepository(AppDbContext db)
        : base(db)
    { }

    public async Task<Currency?> GetByIdAsync(int id)
    {
        return await _db.Currencies.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(Currency currency)
    {
        await _db.Currencies.AddAsync(currency);

        await _db.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(Currency currency)
    {
        _db.Currencies.Update(currency);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task DeleteAsync(Currency currency)
    {
        _db.Currencies.Remove(currency);

        await _db.SaveChangesAsync();
    }
}
