using ShagOxServer.Application.Interfaces.Services.Orders;

namespace ShagOxServer.Application.Services.Orders;
public class OrderNumberGenerator
    : IOrderNumberGenerator
{
    public string Generate()
    {
        var date = DateTime.UtcNow.ToString("yyyyMMdd");

        var random = Guid.NewGuid()
            .ToString("N")[..6]
            .ToUpperInvariant();

        return $"ORD-{date}-{random}";
    }
}
