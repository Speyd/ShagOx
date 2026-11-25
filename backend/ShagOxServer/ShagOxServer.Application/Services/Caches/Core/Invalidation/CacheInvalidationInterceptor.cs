using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ShagOxServer.Application.DTOs.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Cache;

namespace ShagOxServer.Application.Services.Caches.Core.Invalidation;
public class CacheInvalidationInterceptor : SaveChangesInterceptor
{
    private readonly ICacheInvalidationDispatcher _dispatcher;
    private readonly ICacheInvalidationState _state;

    private readonly Dictionary<DbContext,
        List<PendingCacheInvalidation>> _currentChanges = new();

    public CacheInvalidationInterceptor(
        ICacheInvalidationDispatcher dispatcher,
        ICacheInvalidationState state)
    {
        _dispatcher = dispatcher;
        _state = state;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;

        if (context is null)
            return base.SavingChangesAsync(
                eventData, result, cancellationToken);

        var changes = context.ChangeTracker
            .Entries()
            .Where(e =>
                e.Entity is IEntityCache &&
                (e.State == EntityState.Added ||
                 e.State == EntityState.Modified ||
                 e.State == EntityState.Deleted))
            .Select(e =>
            {
                var entity = (IEntityCache)e.Entity;

                return new PendingCacheInvalidation(
                    entity,
                    e.State,
                    entity.GetCacheContext()
                );
            })
            .ToList();

        _currentChanges[context] = changes;

        return base.SavingChangesAsync(
            eventData, result, cancellationToken);
    }

    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;

        if (context is null)
            return result;

        if (!_currentChanges.Remove(context, out var changes))
            return result;

        if (context.Database.CurrentTransaction is not null)
        {
            _state.Add(context, changes);

            return result;
        }

        await InvalidateAsync(changes, cancellationToken);

        return result;
    }

    public override Task SaveChangesFailedAsync(
        DbContextErrorEventData eventData,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is { } context)
            _currentChanges.Remove(context);

        return base.SaveChangesFailedAsync(
            eventData, cancellationToken);
    }

    private async Task InvalidateAsync(
        IEnumerable<PendingCacheInvalidation> changes,
        CancellationToken cancellationToken)
    {
        foreach (var change in changes)
        {
            await _dispatcher.InvalidateAsync(
                change.Entity,
                change.CacheContext,
                change.State,
                cancellationToken);
        }
    }
}
