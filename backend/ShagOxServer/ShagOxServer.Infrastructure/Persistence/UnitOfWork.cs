using Microsoft.EntityFrameworkCore.Storage;
using ShagOxServer.Application.Interfaces.Persistences;

namespace ShagOxServer.Infrastructure.Persistence;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _db;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(
        AppDbContext db,
        IDbContextTransaction? transaction)
    {
        _db = db;
        _transaction = transaction;
    }

    public async Task BeginTransactionAsync()
    {
        _ = await _db.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        await _db.SaveChangesAsync();
        await _transaction!.CommitAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }

    public async Task RollbackAsync()
    {
        if (_transaction != null)
            await _transaction.RollbackAsync();
    }
}
