using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Systems.Middleware;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Services.Caches.Keys.Security;
using ShagOxServer.Domain.Entities.Security;
using System.Net;

namespace ShagOxServer.Api.Middleware;
public sealed class RequestTrackingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly RequestTrackingOptions _trackingOptions;

    
    public RequestTrackingMiddleware(
        RequestDelegate next,
        IOptions<RequestTrackingOptions> trackingOptions)
    {
        _next = next;
        _trackingOptions = trackingOptions.Value;
    }


    public async Task InvokeAsync(
        HttpContext context,
        IUserCacheService userCache,
        IUserDeviceCommandsRepository deviceCommands)
    {
        var cfIp = context.Request.Headers["CF-Connecting-IP"]
            .ToString();

        IPAddress? userIp = null;

        if (IPAddress.TryParse(cfIp, out var parsedIp))
        {
            userIp = parsedIp;
        }
        else
        {
            userIp = context.Connection.RemoteIpAddress;
        }

        var ipAddress = userIp?.ToString();

        if (string.IsNullOrWhiteSpace(ipAddress))
        {
            await _next(context);
            return;
        }
        var cacheKey = IpRateLimitStateKeys.ByIp(ipAddress);

        var state = await userCache
            .GetAsync<IpRateLimitState>(cacheKey);

        state ??= new IpRateLimitState(ipAddress);

        var now = DateTime.UtcNow;

        if (IsBanned(state, now))
        {
            await RejectRequestAsync(context, state, now);
            return;
        }

        ResetExpiredBan(state, now);

        await UpdateRequestState(state, now, deviceCommands);

        await userCache.SetAsync(
            cacheKey,
            state,
            _trackingOptions.CacheDuration);

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

    private async Task UpdateRequestState(
        IpRateLimitState state,
        DateTime now,
        IUserDeviceCommandsRepository deviceCommands)
    {
        state.HttpRequest++;

        if (state.HttpRequest % _trackingOptions.RequestsPerToken != 0)
            return;

        state.RemainingTokens++;

        if (state.RemainingTokens < _trackingOptions.MaxTokens)
            return;

        state.RemainingTokens = 0;

        var previousLevel = state.BanLevel;

        state.BanLevel = Math.Min(
            state.BanLevel + 1,
            _trackingOptions.BanLevelCount);

        state.BannedUntil = now.Add(
            TimeSpan.FromTicks(
                _trackingOptions.BanDuration.Ticks * state.BanLevel));

        if (previousLevel < _trackingOptions.BanLevelCount &&
            state.BanLevel == _trackingOptions.BanLevelCount)
        {
            await deviceCommands.BanDeviceByIpAsync(
                state.Ip,
                null);
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
