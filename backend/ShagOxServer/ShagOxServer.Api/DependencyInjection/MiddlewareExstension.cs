using ShagOxServer.Api.Middleware;
using ShagOxServer.Api.Middleware.Users;

namespace ShagOxServer.Api.DependencyInjection;
public static class MiddlewareExstension
{
    public static WebApplication UseMiddleware(
        this WebApplication app)
    {
        app.UseMiddleware<RequestTrackingMiddleware>();

        app.UseMiddleware<DeviceAccessMiddleware>();

        app.UseMiddleware<BlockedDeviceMiddleware>();

        app.UseMiddleware<BlockedSessionMiddleware>();

        app.UseMiddleware<UpdateLastSeenMiddleware>();


        return app;
    }
}
