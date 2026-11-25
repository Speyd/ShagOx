using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Systems;

namespace ShagOxServer.Application.Common.Builders;
public class OrderUrlBuilder
{
    private readonly FrontendOptions _frontendOptions;

    public OrderUrlBuilder(
        IOptions<FrontendOptions> options)
    {
        _frontendOptions = options.Value;
    }

    public string BuildOrderDetailsUrl(long orderId)
    {
        return $"{_frontendOptions.BaseUrl.TrimEnd('/')}" +
               $"{_frontendOptions.OrderDetailsPath.TrimEnd('/')}/{orderId}";
    }
}
