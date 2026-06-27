using ShagOxServer.Api.Middleware;

namespace ShagOxServer.Api.DependencyInjection;

public static class PipelineExtensions
{
    public static WebApplication ConfigurePipeline(
        this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors("Frontend");

        app.UseAuthentication();

        app.UseMiddleware<UpdateLastSeenMiddleware>();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}