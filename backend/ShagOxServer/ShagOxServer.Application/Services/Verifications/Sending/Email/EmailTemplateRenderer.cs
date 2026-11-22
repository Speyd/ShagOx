using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Localization;
using RazorLight;
using ShagOxServer.Application.DTOs.Orders.Core.Query.Email;
using ShagOxServer.Application.Resources.Orders.OrderPaidEmail;

namespace ShagOxServer.Application.Services.Verifications.Sending.Email;
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
            .SetOperatingAssembly(
                typeof(OrderPaidEmailResources).Assembly
            )
            .UseMemoryCachingProvider()
            .Build();
    }

    public static OrderPaidEmailModel PopulateLocalizedFields(
        OrderPaidEmailModel model)
    {
        return model with
        {
            PaymentSuccess = OrderPaidEmailResources.PaymentSuccess,
            ThankYou = OrderPaidEmailResources.ThankYou,
            OrderTitle = OrderPaidEmailResources.Order,

            Greeting = string.Format(
                OrderPaidEmailResources.Greeting,
                model.CustomerName
            ),

            YourProducts = OrderPaidEmailResources.YourProducts,
            Quantity = OrderPaidEmailResources.Quantity,
            Products = OrderPaidEmailResources.Products,
            Delivery = OrderPaidEmailResources.Delivery,
            Total = OrderPaidEmailResources.Total,
            ViewOrder = OrderPaidEmailResources.ViewOrder,
            Footer = OrderPaidEmailResources.Footer
        };

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
