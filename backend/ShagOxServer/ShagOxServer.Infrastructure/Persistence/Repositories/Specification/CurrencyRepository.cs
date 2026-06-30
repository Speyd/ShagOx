using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Infrastructure.Interfaces.Specification;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification;
public class CurrencyRepository : BaseRepository, ICurrencyRepository
{
    public CurrencyRepository(AppDbContext db)
        : base(db)
    { }

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


    public async Task<Currency?> GetByCodeAsync(string code)
    {
        return await _db.Currencies.FirstOrDefaultAsync(c => c.Code == code);
    }

    public async Task<Currency?> GetByIdAsync(int id)
    {
        return await _db.Currencies.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Currency?> GetBySymbolAsync(string symbol)
    {
        return await _db.Currencies.FirstOrDefaultAsync(c => c.Symbol == symbol);
    }

    public async Task<bool> ExistsByCodeAsync(string? code)
    {
        return await _db.Currencies.AnyAsync(c => c.Code == code);
    }

    public async Task<bool> ExistsBySymbolAsync(string? symbol)
    {
        return await _db.Currencies.AnyAsync(c => c.Symbol == symbol);
    }

    public async Task<bool> ExistsByNameAsync(string? name)
    {
        return await _db.Currencies.AnyAsync(c => c.Name == name);
    }
}
