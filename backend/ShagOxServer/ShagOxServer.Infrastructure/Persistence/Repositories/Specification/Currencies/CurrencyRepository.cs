using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Currencies;
public class CurrencyRepository 
    : BaseRepository, ICurrencyRepository
{
    public CurrencyRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<Currency?> GetByIdAsync(int id)
    {
        return await _db.Currencies
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public void Add(Currency currency)
    {
         _db.Currencies.Add(currency);
    }

    public bool Update(Currency currency)
    {
        _db.Currencies.Update(currency);
        return true;
    }

    public void Delete(Currency currency)
    {
        _db.Currencies.Remove(currency);
    }
}