using Microsoft.EntityFrameworkCore.Storage;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;

namespace ShagOxServer.Infrastructure.Persistence;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _db;
    private IDbContextTransaction? _transaction;


    public UnitOfWork(AppDbContext db)
    {
        _db = db;
    }


    public async Task BeginTransactionAsync()
    {
        _transaction = await _db.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        var saved = await _db.SaveChangesAsync();
        Console.WriteLine($"Saved rows: {saved}");

        Console.WriteLine($"UoW context: {_db.ContextId}");
        Console.WriteLine(_db.ChangeTracker.DebugView.LongView);

        if (_transaction is not null)
            await _transaction!.CommitAsync();
    }

    public void DebugTrackedEntities()
    {
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _db.SaveChangesAsync();
    }

    public async Task RollbackAsync()
    {
        if (_transaction is not null)
            await _transaction.RollbackAsync();
    }
}
