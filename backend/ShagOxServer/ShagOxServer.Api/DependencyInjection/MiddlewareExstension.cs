using ShagOxServer.Api.Middleware;

namespace ShagOxServer.Api.DependencyInjection;
public static class MiddlewareExstension
{
    public static WebApplication UseMiddleware(
        this WebApplication app)
    {

        //app.UseMiddleware<RequestTrackingMiddleware>();

        app.UseMiddleware<UpdateLastSeenMiddleware>();


        return app;
    }
}
