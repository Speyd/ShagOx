using Microsoft.EntityFrameworkCore.Diagnostics;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Domain.Cache;
using System.Data.Common;

namespace ShagOxServer.Application.Services.Caches.Core.Invalidation;
public class CacheTransactionInterceptor : DbTransactionInterceptor
{
    private readonly ICacheInvalidationState _state;
    private readonly ICacheInvalidationDispatcher _dispatcher;

    public CacheTransactionInterceptor(
        ICacheInvalidationState state,
        ICacheInvalidationDispatcher dispatcher)
    {
        _state = state;
        _dispatcher = dispatcher;
    }

    public override async Task TransactionCommittedAsync(
        DbTransaction transaction,
        TransactionEndEventData eventData,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;

        if (context is not null)
        {
            var changes = _state.Get(context);

            _state.Clear(context);

            foreach (var change in changes)
            {
                await _dispatcher.InvalidateAsync(
                    change.Entity,
                    change.CacheContext,
                    change.State,
                    cancellationToken);
            }
        }

        await base.TransactionCommittedAsync(
            transaction, eventData, cancellationToken);
    }

    public override async Task TransactionRolledBackAsync(
        DbTransaction transaction,
        TransactionEndEventData eventData,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is { } context)
            _state.Clear(context);

        await base.TransactionRolledBackAsync(
            transaction, eventData, cancellationToken);
    }
}
