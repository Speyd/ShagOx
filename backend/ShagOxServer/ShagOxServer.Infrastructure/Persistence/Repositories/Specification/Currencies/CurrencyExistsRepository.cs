using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Currencies;
public class CurrencyExistsRepository : BaseRepository, ICurrencyExistsRepository
{
    public CurrencyExistsRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await _db.Currencies.AnyAsync(c => c.Id == id);
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