using Microsoft.EntityFrameworkCore.Storage;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;

namespace ShagOxServer.Infrastructure.Persistence;
/// <summary>
/// Підтримує вкладені виклики: сервіс може відкрити транзакцію і викликати інший
/// сервіс, який теж відкриває свою (наприклад, створення відгуку → створення фото).
/// Реальна транзакція БД одна: внутрішні Begin/Commit лише зберігають зміни,
/// комітить зовнішній рівень, а Rollback на будь-якому рівні відкочує все.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _db;
    private IDbContextTransaction? _transaction;
    private int _depth;


    public UnitOfWork(AppDbContext db)
    {
        _db = db;
    }

    public async Task BeginTransactionAsync()
    {
        if (_depth++ > 0)
            return;

        _transaction = await _db.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        await _db.SaveChangesAsync();

        if (_depth > 0 && --_depth > 0)
            return;

        if (_transaction is not null)
        {
            await _transaction.CommitAsync();
            await DisposeTransactionAsync();
        }
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }

    public async Task RollbackAsync()
    {
        _depth = 0;

        if (_transaction is not null)
        {
            await _transaction.RollbackAsync();
            await DisposeTransactionAsync();
        }
    }

    private async Task DisposeTransactionAsync()
    {
        await _transaction!.DisposeAsync();
        _transaction = null;
    }
}
