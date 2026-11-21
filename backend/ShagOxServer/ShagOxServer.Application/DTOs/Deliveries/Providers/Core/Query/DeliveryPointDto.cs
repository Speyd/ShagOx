using ShagOxServer.Domain.Entities.Deliveries.Enum;

namespace ShagOxServer.Application.DTOs.Deliveries.Providers.Core.Query;
public class DeliveryPointDto
{
    public string ExternalId { get; set; } = null!;
    public string Number { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public DeliveryPointType Type { get; set; }
}
