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
        DebugTrackedEntities();
        await _db.SaveChangesAsync();
        if(_transaction is not null)
            await _transaction!.CommitAsync();
    }

    public void DebugTrackedEntities()
    {
        //_db.ChangeTracker.AutoDetectChangesEnabled = false;

        //try
        //{
        //    foreach (var entry in _db.ChangeTracker.Entries())
        //    {
        //        foreach (var navigation in entry.Navigations)
        //        {
        //            if (!navigation.Metadata.IsCollection)
        //                continue;

        //            var collection = navigation.CurrentValue
        //                as System.Collections.IEnumerable;

        //            if (collection is null)
        //                continue;

        //            foreach (var item in collection)
        //            {
        //                if (item is Order order)
        //                {
        //                    Console.WriteLine(
        //                        $"Owner: {entry.Metadata.ClrType.Name}, " +
        //                        $"Navigation: {navigation.Metadata.Name}, " +
        //                        $"OrderId: {order.Id}, " +
        //                        $"Hash: {RuntimeHelpers.GetHashCode(order)}");
        //                }
        //            }
        //        }
        //    }
        //}
        //finally
        //{
        //    _db.ChangeTracker.AutoDetectChangesEnabled = true;
        //}
    }

    public async Task<int> SaveChangesAsync()
    {
        DebugTrackedEntities();
        return await _db.SaveChangesAsync();
    }

    public async Task RollbackAsync()
    {
        if (_transaction is not null)
            await _transaction.RollbackAsync();
    }
}
