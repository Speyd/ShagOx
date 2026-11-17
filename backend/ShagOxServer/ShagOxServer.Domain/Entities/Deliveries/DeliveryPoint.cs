using ShagOxServer.Domain.Entities.Deliveries.Enum;

namespace ShagOxServer.Domain.Entities.Deliveries;
public class DeliveryPoint
{
    public string ExternalId { get; set; } = null!;
    public string Number { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public DeliveryPointType Type { get; set; }
}
