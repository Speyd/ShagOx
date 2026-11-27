using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ShagOxServer.Application.DTOs.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Cache;
using System.Collections.Concurrent;

namespace ShagOxServer.Application.Services.Caches.Core.Invalidation;
public sealed class CacheInvalidationInterceptor : SaveChangesInterceptor
{
    private readonly ICacheInvalidationDispatcher _dispatcher;
    private readonly ICacheInvalidationState _state;

    private readonly ConcurrentDictionary<
        DbContext,
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
        {
            return base.SavingChangesAsync(
                eventData,
                result,
                cancellationToken);
        }

        var changes = CollectChanges(context);

        _currentChanges[context] = changes;

        return base.SavingChangesAsync(
            eventData,
            result,
            cancellationToken);
    }

    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;

        if (context is null)
            return result;

        if (!_currentChanges.TryRemove(context, out var changes))
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
        {
            _currentChanges.TryRemove(context, out _);
        }

        return base.SaveChangesFailedAsync(
            eventData,
            cancellationToken);
    }

    public override Task SaveChangesCanceledAsync(
        DbContextEventData eventData,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is { } context)
        {
            _currentChanges.TryRemove(context, out _);
        }

        return base.SaveChangesCanceledAsync(
            eventData,
            cancellationToken);
    }


    private static List<PendingCacheInvalidation> CollectChanges(
    DbContext context)
    {
        var tracker = context.ChangeTracker;
        var previous = tracker.AutoDetectChangesEnabled;

        try
        {
            tracker.AutoDetectChangesEnabled = false;

            return tracker.Entries()
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
                        entity.GetCacheContext());
                })
                .ToList();
        }
        finally
        {
            tracker.AutoDetectChangesEnabled = previous;
        }
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
