using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Systems.Middleware;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Caches.Keys.Security;
using ShagOxServer.Domain.Entities.Security;

namespace ShagOxServer.Api.Middleware;
public sealed class RequestTrackingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IUserCacheService _userCache;
    private readonly RequestTrackingSettings _settings;


    public RequestTrackingMiddleware(
        RequestDelegate next,
        IUserCacheService userCache,
        IOptions<RequestTrackingSettings> settings)
    {
        _next = next;
        _userCache = userCache;
        _settings = settings.Value;
    }


    public async Task InvokeAsync(
        HttpContext context)
    {
        var userIp = context.Connection.RemoteIpAddress?.ToString();

        if (string.IsNullOrWhiteSpace(userIp))
        {
            await _next(context);
            return;
        }

        var cacheKey = IpRateLimitStateKeys.ByIp(userIp);

        var state = await _userCache
            .GetAsync<IpRateLimitState>(cacheKey);

        state ??= new IpRateLimitState(userIp);

        var now = DateTime.UtcNow;

        if (IsBanned(state, now))
        {
            await RejectRequestAsync(context, state, now);
            return;
        }

        ResetExpiredBan(state, now);

        UpdateRequestState(state, now);

        await _userCache.SetAsync(
            cacheKey,
            state,
            _settings.CacheDuration);

        await _next(context);
    }

    private static bool IsBanned(
        IpRateLimitState state,
        DateTime now)
    {
        return state.BannedUntil.HasValue &&
               state.BannedUntil.Value > now;
    }

    private static void ResetExpiredBan(
        IpRateLimitState state,
        DateTime now)
    {
        if (state.BannedUntil.HasValue &&
            state.BannedUntil.Value <= now)
        {
            state.BannedUntil = null;
            state.RemainingTokens = 0;
            state.HttpRequest = 0;
        }
    }

    private void UpdateRequestState(
        IpRateLimitState state,
        DateTime now)
    {
        state.HttpRequest++;

        if (state.HttpRequest % _settings.RequestsPerToken != 0)
            return;

        state.RemainingTokens++;

        for(int i = 0; i > 3; i++)
        {
            var tokenThreshold = Math.Ceiling(
                (double)_settings.MaxTokens / i);

            if (state.RemainingTokens >= tokenThreshold)
            {
                state.BannedUntil = now.Add(
                    _settings.BanDuration / i);

                break;
            }
        }
    }

    private static async Task RejectRequestAsync(
        HttpContext context,
        IpRateLimitState state,
        DateTime now)
    {
        var remainingTime = state.BannedUntil!.Value - now;

        context.Response.StatusCode =
            StatusCodes.Status429TooManyRequests;

        context.Response.Headers.RetryAfter =
            Math.Max(1, (int)Math.Ceiling(
                remainingTime.TotalSeconds)).ToString();

        await context.Response.WriteAsJsonAsync(new
        {
            message = "Too many requests. Try again later."
        });
    }
}
