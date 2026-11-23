using RazorLight;

namespace ShagOxServer.Application.Services.Verifications.Sending.Email.Processing;
public class EmailTemplateRenderer
{
    private readonly RazorLightEngine _engine;

    public EmailTemplateRenderer()
    {
        var templatePath = Path.Combine(
            AppContext.BaseDirectory,
            "Templates",
            "EmailTemplates"
        );

        _engine = new RazorLightEngineBuilder()
            .UseFileSystemProject(templatePath)
            .UseMemoryCachingProvider()
            .Build();
    }

    public async Task<string> RenderAsync<T>(
        string templateName,
        T model)
    {
        return await _engine.CompileRenderAsync(
            templateName,
            model);
    }
}
