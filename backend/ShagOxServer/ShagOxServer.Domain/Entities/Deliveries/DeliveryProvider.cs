using ShagOxServer.Domain.Base;

namespace ShagOxServer.Domain.Entities.Deliveries;
public class DeliveryProvider
     : BaseTranslatable
{
    public string Code { get; set; } = null!;


    public override string GetIdentificator()
    {
        return Code;
    }

    public override string ToString()
    {
        return $"Id: {Id} | Code: {Code}";
    }
}
